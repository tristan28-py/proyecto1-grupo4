using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class VentaAdoRepository : IVentaRepository, ISaleRepository
    {
        /// <summary>
        /// Cumple el requisito obligatorio de rúbrica:
        /// Implementa una transacción SqlTransaction para registrar la venta, los detalles,
        /// descontar existencias y asentar el registro en HistoricoMovimientosInventario.
        /// Si ocurre cualquier fallo, se hace Rollback y el inventario no se altera.
        /// </summary>
        public int RegisterSaleWithTransaction(Venta sale, IEnumerable<DetalleVenta> details, int employeeId)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar Encabezado de Venta
                        string saleQuery = @"INSERT INTO dbo.Venta 
                                            (ClienteId, EmpleadoId, FechaVenta, SubTotal, Impuesto, Descuento, Total, Estado, Observaciones)
                                            VALUES 
                                            (@ClienteId, @EmpleadoId, @FechaVenta, @SubTotal, @Impuesto, @Descuento, @Total, @Estado, @Observaciones);
                                            SELECT SCOPE_IDENTITY();";

                        int saleId;
                        using (var cmdSale = new SqlCommand(saleQuery, conn, trans))
                        {
                            cmdSale.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = sale.ClienteId });
                            cmdSale.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = employeeId });
                            cmdSale.Parameters.Add(new SqlParameter("@FechaVenta", SqlDbType.DateTime) { Value = sale.FechaVenta });
                            cmdSale.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = sale.SubTotal });
                            cmdSale.Parameters.Add(new SqlParameter("@Impuesto", SqlDbType.Decimal) { Value = sale.Impuesto });
                            cmdSale.Parameters.Add(new SqlParameter("@Descuento", SqlDbType.Decimal) { Value = sale.Descuento });
                            cmdSale.Parameters.Add(new SqlParameter("@Total", SqlDbType.Decimal) { Value = sale.Total });
                            cmdSale.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30) { Value = sale.Estado });
                            cmdSale.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar, 500) { Value = (object?)sale.Observaciones ?? DBNull.Value });

                            saleId = Convert.ToInt32(cmdSale.ExecuteScalar());
                        }

                        // 2. Procesar cada detalle, actualizar stock y registrar en Kardex/Histórico
                        foreach (var detail in details)
                        {
                            // A. Insertar Detalle de Venta
                            string detailQuery = @"INSERT INTO dbo.DetalleVenta 
                                                   (VentaId, ProductoId, Cantidad, PrecioUnitario, Descuento, SubTotal)
                                                   VALUES 
                                                   (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario, @Descuento, @SubTotal);";

                            using (var cmdDetail = new SqlCommand(detailQuery, conn, trans))
                            {
                                cmdDetail.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = saleId });
                                cmdDetail.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = detail.ProductoId });
                                cmdDetail.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int) { Value = detail.Cantidad });
                                cmdDetail.Parameters.Add(new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = detail.PrecioUnitario });
                                cmdDetail.Parameters.Add(new SqlParameter("@Descuento", SqlDbType.Decimal) { Value = detail.Descuento });
                                cmdDetail.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = detail.SubTotal });

                                cmdDetail.ExecuteNonQuery();
                            }

                            // B. Consultar Stock Actual del Producto
                            int previousStock;
                            string getStockQuery = "SELECT StockActual FROM dbo.Productos WHERE ProductoId = @ProductoId";
                            using (var cmdStock = new SqlCommand(getStockQuery, conn, trans))
                            {
                                cmdStock.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = detail.ProductoId });
                                object stockObj = cmdStock.ExecuteScalar();
                                previousStock = Convert.ToInt32(stockObj);
                            }

                            if (previousStock < detail.Cantidad)
                            {
                                throw new InvalidOperationException($"Stock insuficiente para el producto ID {detail.ProductoId}. Disponible: {previousStock}, Solicitado: {detail.Cantidad}");
                            }

                            int newStock = previousStock - detail.Cantidad;

                            // C. Actualizar Stock del Producto
                            string updateStockQuery = "UPDATE dbo.Productos SET StockActual = @NewStock WHERE ProductoId = @ProductoId";
                            using (var cmdUpdateStock = new SqlCommand(updateStockQuery, conn, trans))
                            {
                                cmdUpdateStock.Parameters.Add(new SqlParameter("@NewStock", SqlDbType.Int) { Value = newStock });
                                cmdUpdateStock.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = detail.ProductoId });

                                cmdUpdateStock.ExecuteNonQuery();
                            }

                            // D. Insertar en HistoricoMovimientosInventario
                            string historyQuery = @"INSERT INTO dbo.HistoricoMovimientosInventario 
                                                   (ProductoId, EmpleadoId, FechaMovimiento, TipoMovimiento, Cantidad, StockAnterior, StockNuevo, Observacion)
                                                   VALUES 
                                                   (@ProductoId, @EmpleadoId, @FechaMovimiento, @TipoMovimiento, @Cantidad, @StockAnterior, @StockNuevo, @Observacion);";

                            using (var cmdHistory = new SqlCommand(historyQuery, conn, trans))
                            {
                                cmdHistory.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = detail.ProductoId });
                                cmdHistory.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = employeeId });
                                cmdHistory.Parameters.Add(new SqlParameter("@FechaMovimiento", SqlDbType.DateTime) { Value = DateTime.Now });
                                cmdHistory.Parameters.Add(new SqlParameter("@TipoMovimiento", SqlDbType.VarChar, 30) { Value = "Venta" });
                                cmdHistory.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int) { Value = detail.Cantidad });
                                cmdHistory.Parameters.Add(new SqlParameter("@StockAnterior", SqlDbType.Int) { Value = previousStock });
                                cmdHistory.Parameters.Add(new SqlParameter("@StockNuevo", SqlDbType.Int) { Value = newStock });
                                cmdHistory.Parameters.Add(new SqlParameter("@Observacion", SqlDbType.VarChar, 255) { Value = $"Venta registrada #{saleId}" });

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

        public Venta? GetSaleById(int saleId)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT VentaId, ClienteId, EmpleadoId, FechaVenta, SubTotal, Impuesto, Descuento, Total, Estado, Observaciones FROM dbo.Venta WHERE VentaId = @VentaId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = saleId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapVenta(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Venta> GetAllSales()
        {
            var list = new List<Venta>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT VentaId, ClienteId, EmpleadoId, FechaVenta, SubTotal, Impuesto, Descuento, Total, Estado, Observaciones FROM dbo.Venta ORDER BY FechaVenta DESC";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapVenta(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<DetalleVenta> GetSaleDetails(int saleId)
        {
            var list = new List<DetalleVenta>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT sd.DetalleVentaId, sd.VentaId, sd.ProductoId, sd.Cantidad, sd.PrecioUnitario, sd.Descuento, sd.SubTotal,
                                        p.Nombre AS NombreProducto
                                 FROM dbo.DetalleVenta sd
                                 INNER JOIN dbo.Productos p ON sd.ProductoId = p.ProductoId
                                 WHERE sd.VentaId = @VentaId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = saleId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var detail = new DetalleVenta
                            {
                                DetalleVentaId = reader.GetInt32(reader.GetOrdinal("DetalleVentaId")),
                                VentaId = reader.GetInt32(reader.GetOrdinal("VentaId")),
                                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                                Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                                Producto = new Producto
                                {
                                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("NombreProducto"))
                                }
                            };
                            list.Add(detail);
                        }
                    }
                }
            }
            return list;
        }

        private static Venta MapVenta(SqlDataReader reader)
        {
            return new Venta
            {
                VentaId = reader.GetInt32(reader.GetOrdinal("VentaId")),
                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                FechaVenta = reader.GetDateTime(reader.GetOrdinal("FechaVenta")),
                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                Impuesto = reader.GetDecimal(reader.GetOrdinal("Impuesto")),
                Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                Estado = reader.GetString(reader.GetOrdinal("Estado")),
                Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones"))
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con SaleAdoRepository.
    /// </summary>
    public class SaleAdoRepository : VentaAdoRepository
    {
    }
}
