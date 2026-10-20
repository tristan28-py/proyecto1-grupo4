using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class ProductAdoRepository : IProductRepository
    {
        public IEnumerable<Product> GetAll()
        {
            var list = new List<Product>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT p.ProductId, p.Name, p.Description, p.PurchasePrice, p.SalePrice, 
                                        p.CurrentStock, p.MinimumStock, p.CategoryId, p.RegistrationDate, p.IsActive,
                                        c.Name AS CategoryName
                                 FROM Products p
                                 INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                                 ORDER BY p.Name";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProduct(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            var list = new List<Product>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT p.ProductId, p.Name, p.Description, p.PurchasePrice, p.SalePrice, 
                                        p.CurrentStock, p.MinimumStock, p.CategoryId, p.RegistrationDate, p.IsActive,
                                        c.Name AS CategoryName
                                 FROM Products p
                                 INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                                 WHERE p.IsActive = 1
                                 ORDER BY p.Name";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProduct(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Product? GetById(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT p.ProductId, p.Name, p.Description, p.PurchasePrice, p.SalePrice, 
                                        p.CurrentStock, p.MinimumStock, p.CategoryId, p.RegistrationDate, p.IsActive,
                                        c.Name AS CategoryName
                                 FROM Products p
                                 INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                                 WHERE p.ProductId = @ProductId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProduct(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            var list = new List<Product>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT p.ProductId, p.Name, p.Description, p.PurchasePrice, p.SalePrice, 
                                        p.CurrentStock, p.MinimumStock, p.CategoryId, p.RegistrationDate, p.IsActive,
                                        c.Name AS CategoryName
                                 FROM Products p
                                 INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                                 WHERE p.IsActive = 1 AND (p.Name LIKE @Search OR p.Description LIKE @Search)
                                 ORDER BY p.Name";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.NVarChar, 100) { Value = $"%{searchTerm}%" });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProduct(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var list = new List<Product>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT p.ProductId, p.Name, p.Description, p.PurchasePrice, p.SalePrice, 
                                        p.CurrentStock, p.MinimumStock, p.CategoryId, p.RegistrationDate, p.IsActive,
                                        c.Name AS CategoryName
                                 FROM Products p
                                 INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                                 WHERE p.CategoryId = @CategoryId AND p.IsActive = 1
                                 ORDER BY p.Name";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = categoryId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProduct(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Product entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"INSERT INTO Products 
                                 (Name, Description, PurchasePrice, SalePrice, CurrentStock, MinimumStock, CategoryId, RegistrationDate, IsActive)
                                 VALUES 
                                 (@Name, @Description, @PurchasePrice, @SalePrice, @CurrentStock, @MinimumStock, @CategoryId, @RegistrationDate, @IsActive);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 150) { Value = entity.Name });
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = (object?)entity.Description ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@PurchasePrice", SqlDbType.Decimal) { Value = entity.PurchasePrice });
                    cmd.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Decimal) { Value = entity.SalePrice });
                    cmd.Parameters.Add(new SqlParameter("@CurrentStock", SqlDbType.Int) { Value = entity.CurrentStock });
                    cmd.Parameters.Add(new SqlParameter("@MinimumStock", SqlDbType.Int) { Value = entity.MinimumStock });
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = entity.CategoryId });
                    cmd.Parameters.Add(new SqlParameter("@RegistrationDate", SqlDbType.DateTime) { Value = entity.RegistrationDate });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public bool Update(Product entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"UPDATE Products 
                                 SET Name = @Name, Description = @Description, PurchasePrice = @PurchasePrice,
                                     SalePrice = @SalePrice, CurrentStock = @CurrentStock, MinimumStock = @MinimumStock,
                                     CategoryId = @CategoryId, IsActive = @IsActive
                                 WHERE ProductId = @ProductId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = entity.ProductId });
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 150) { Value = entity.Name });
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = (object?)entity.Description ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@PurchasePrice", SqlDbType.Decimal) { Value = entity.PurchasePrice });
                    cmd.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Decimal) { Value = entity.SalePrice });
                    cmd.Parameters.Add(new SqlParameter("@CurrentStock", SqlDbType.Int) { Value = entity.CurrentStock });
                    cmd.Parameters.Add(new SqlParameter("@MinimumStock", SqlDbType.Int) { Value = entity.MinimumStock });
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = entity.CategoryId });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            // Borrado lógico con IsActive = 0
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "UPDATE Products SET IsActive = 0 WHERE ProductId = @ProductId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateStock(int productId, int quantityChange)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "UPDATE Products SET CurrentStock = CurrentStock + @Change WHERE ProductId = @ProductId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Change", SqlDbType.Int) { Value = quantityChange });
                    cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Product MapProduct(SqlDataReader reader)
        {
            var product = new Product
            {
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                PurchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice")),
                SalePrice = reader.GetDecimal(reader.GetOrdinal("SalePrice")),
                CurrentStock = reader.GetInt32(reader.GetOrdinal("CurrentStock")),
                MinimumStock = reader.GetInt32(reader.GetOrdinal("MinimumStock")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };

            if (!reader.IsDBNull(reader.GetOrdinal("CategoryName")))
            {
                product.Category = new Category
                {
                    CategoryId = product.CategoryId,
                    Name = reader.GetString(reader.GetOrdinal("CategoryName"))
                };
            }

            return product;
        }
    }
}