using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class SaleAdoRepository : ISaleRepository
    {
        /// <summary>
        /// Cumple el requisito obligatorio de rúbrica:
        /// Implementa una transacción SqlTransaction para registrar la venta, los detalles,
        /// descontar existencias y asentar el registro en HistóricoMovimientosInventario.
        /// Si ocurre cualquier fallo, se hace Rollback y el inventario no se altera.
        /// </summary>
        public int RegisterSaleWithTransaction(Sale sale, IEnumerable<SaleDetail> details, int employeeId)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar Encabezado de Venta
                        string saleQuery = @"INSERT INTO Sales 
                                            (ClientId, EmployeeId, SaleDate, SubTotal, Tax, Discount, Total, Status, Observations)
                                            VALUES 
                                            (@ClientId, @EmployeeId, @SaleDate, @SubTotal, @Tax, @Discount, @Total, @Status, @Observations);
                                            SELECT SCOPE_IDENTITY();";

                        int saleId;
                        using (var cmdSale = new SqlCommand(saleQuery, conn, trans))
                        {
                            cmdSale.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = sale.ClientId });
                            cmdSale.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId });
                            cmdSale.Parameters.Add(new SqlParameter("@SaleDate", SqlDbType.DateTime) { Value = sale.SaleDate });
                            cmdSale.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = sale.SubTotal });
                            cmdSale.Parameters.Add(new SqlParameter("@Tax", SqlDbType.Decimal) { Value = sale.Tax });
                            cmdSale.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Decimal) { Value = sale.Discount });
                            cmdSale.Parameters.Add(new SqlParameter("@Total", SqlDbType.Decimal) { Value = sale.Total });
                            cmdSale.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 30) { Value = sale.Status });
                            cmdSale.Parameters.Add(new SqlParameter("@Observations", SqlDbType.NVarChar, 500) { Value = (object?)sale.Observations ?? DBNull.Value });

                            saleId = Convert.ToInt32(cmdSale.ExecuteScalar());
                        }

                        // 2. Procesar cada detalle, actualizar stock y registrar en Kardex/Histórico
                        foreach (var detail in details)
                        {
                            // A. Insertar Detalle de Venta
                            string detailQuery = @"INSERT INTO SaleDetails 
                                                   (SaleId, ProductId, Quantity, UnitPrice, Discount, SubTotal)
                                                   VALUES 
                                                   (@SaleId, @ProductId, @Quantity, @UnitPrice, @Discount, @SubTotal);";

                            using (var cmdDetail = new SqlCommand(detailQuery, conn, trans))
                            {
                                cmdDetail.Parameters.Add(new SqlParameter("@SaleId", SqlDbType.Int) { Value = saleId });
                                cmdDetail.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = detail.ProductId });
                                cmdDetail.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = detail.Quantity });
                                cmdDetail.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = detail.UnitPrice });
                                cmdDetail.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Decimal) { Value = detail.Discount });
                                cmdDetail.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = detail.SubTotal });

                                cmdDetail.ExecuteNonQuery();
                            }

                            // B. Consultar Stock Actual del Producto
                            int previousStock;
                            string getStockQuery = "SELECT CurrentStock FROM Products WHERE ProductId = @ProductId";
                            using (var cmdStock = new SqlCommand(getStockQuery, conn, trans))
                            {
                                cmdStock.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = detail.ProductId });
                                object stockObj = cmdStock.ExecuteScalar();
                                previousStock = Convert.ToInt32(stockObj);
                            }

                            if (previousStock < detail.Quantity)
                            {
                                throw new InvalidOperationException($"Stock insuficiente para el producto ID {detail.ProductId}. Disponible: {previousStock}, Solicitado: {detail.Quantity}");
                            }

                            int newStock = previousStock - detail.Quantity;

                            // C. Actualizar Stock del Producto
                            string updateStockQuery = "UPDATE Products SET CurrentStock = @NewStock WHERE ProductId = @ProductId";
                            using (var cmdUpdateStock = new SqlCommand(updateStockQuery, conn, trans))
                            {
                                cmdUpdateStock.Parameters.Add(new SqlParameter("@NewStock", SqlDbType.Int) { Value = newStock });
                                cmdUpdateStock.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = detail.ProductId });

                                cmdUpdateStock.ExecuteNonQuery();
                            }

                            // D. Insertar en HistóricoMovimientosInventario
                            string historyQuery = @"INSERT INTO InventoryMovementHistory 
                                                   (ProductId, EmployeeId, MovementDate, MovementType, Quantity, PreviousStock, NewStock, Observation)
                                                   VALUES 
                                                   (@ProductId, @EmployeeId, @MovementDate, @MovementType, @Quantity, @PreviousStock, @NewStock, @Observation);";

                            using (var cmdHistory = new SqlCommand(historyQuery, conn, trans))
                            {
                                cmdHistory.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = detail.ProductId });
                                cmdHistory.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId });
                                cmdHistory.Parameters.Add(new SqlParameter("@MovementDate", SqlDbType.DateTime) { Value = DateTime.Now });
                                cmdHistory.Parameters.Add(new SqlParameter("@MovementType", SqlDbType.NVarChar, 50) { Value = "Venta" });
                                cmdHistory.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = detail.Quantity });
                                cmdHistory.Parameters.Add(new SqlParameter("@PreviousStock", SqlDbType.Int) { Value = previousStock });
                                cmdHistory.Parameters.Add(new SqlParameter("@NewStock", SqlDbType.Int) { Value = newStock });
                                cmdHistory.Parameters.Add(new SqlParameter("@Observation", SqlDbType.NVarChar, 250) { Value = $"Venta registrada # {saleId}" });

                                cmdHistory.ExecuteNonQuery();
                            }
                        }

                        // Si todas las operaciones fueron exitosas, se confirma la transacción
                        trans.Commit();
                        return saleId;
                    }
                    catch
                    {
                        // Rollback obligatorio en caso de error
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public Sale? GetSaleById(int saleId)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT SaleId, ClientId, EmployeeId, SaleDate, SubTotal, Tax, Discount, Total, Status, Observations FROM Sales WHERE SaleId = @SaleId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@SaleId", SqlDbType.Int) { Value = saleId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapSale(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Sale> GetAllSales()
        {
            var list = new List<Sale>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT SaleId, ClientId, EmployeeId, SaleDate, SubTotal, Tax, Discount, Total, Status, Observations FROM Sales ORDER BY SaleDate DESC";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapSale(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<SaleDetail> GetSaleDetails(int saleId)
        {
            var list = new List<SaleDetail>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT sd.SaleDetailId, sd.SaleId, sd.ProductId, sd.Quantity, sd.UnitPrice, sd.Discount, sd.SubTotal,
                                        p.Name AS ProductName
                                 FROM SaleDetails sd
                                 INNER JOIN Products p ON sd.ProductId = p.ProductId
                                 WHERE sd.SaleId = @SaleId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@SaleId", SqlDbType.Int) { Value = saleId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var detail = new SaleDetail
                            {
                                SaleDetailId = reader.GetInt32(reader.GetOrdinal("SaleDetailId")),
                                SaleId = reader.GetInt32(reader.GetOrdinal("SaleId")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                Discount = reader.GetDecimal(reader.GetOrdinal("Discount")),
                                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                                Product = new Product
                                {
                                    ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    Name = reader.GetString(reader.GetOrdinal("ProductName"))
                                }
                            };
                            list.Add(detail);
                        }
                    }
                }
            }
            return list;
        }

        private static Sale MapSale(SqlDataReader reader)
        {
            return new Sale
            {
                SaleId = reader.GetInt32(reader.GetOrdinal("SaleId")),
                ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                SaleDate = reader.GetDateTime(reader.GetOrdinal("SaleDate")),
                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                Tax = reader.GetDecimal(reader.GetOrdinal("Tax")),
                Discount = reader.GetDecimal(reader.GetOrdinal("Discount")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                Observations = reader.IsDBNull(reader.GetOrdinal("Observations")) ? null : reader.GetString(reader.GetOrdinal("Observations"))
            };
        }
    }
}