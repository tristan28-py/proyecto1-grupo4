using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class ProductoAdoRepository : IProductoRepository, IProductRepository
    {
        public IEnumerable<Producto> GetAll()
        {
            var list = new List<Producto>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                        p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                        c.Nombre AS CategoriaNombre
                                 FROM dbo.Productos p
                                 INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                 ORDER BY p.Nombre";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProducto(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Producto> GetActiveProducts()
        {
            var list = new List<Producto>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                        p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                        c.Nombre AS CategoriaNombre
                                 FROM dbo.Productos p
                                 INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                 WHERE p.Activo = 1
                                 ORDER BY p.Nombre";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProducto(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Producto? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                        p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                        c.Nombre AS CategoriaNombre
                                 FROM dbo.Productos p
                                 INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                 WHERE p.ProductoId = @ProductoId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProducto(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Producto> SearchProducts(string searchTerm)
        {
            var list = new List<Producto>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                        p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                        c.Nombre AS CategoriaNombre
                                 FROM dbo.Productos p
                                 INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                 WHERE p.Activo = 1 AND (p.Nombre LIKE @Search OR p.Descripcion LIKE @Search)
                                 ORDER BY p.Nombre";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.VarChar, 100) { Value = $"%{searchTerm}%" });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProducto(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Producto> GetProductsByCategory(int categoryId)
        {
            var list = new List<Producto>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                        p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                        c.Nombre AS CategoriaNombre
                                 FROM dbo.Productos p
                                 INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                 WHERE p.CategoriaId = @CategoriaId AND p.Activo = 1
                                 ORDER BY p.Nombre";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = categoryId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapProducto(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Producto entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"INSERT INTO dbo.Productos 
                                 (Nombre, Descripcion, PrecioCompra, PrecioVenta, StockActual, StockMinimo, CategoriaId, FechaRegistro, Activo)
                                 VALUES 
                                 (@Nombre, @Descripcion, @PrecioCompra, @PrecioVenta, @StockActual, @StockMinimo, @CategoriaId, @FechaRegistro, @Activo);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150) { Value = entity.Nombre });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 500) { Value = (object?)entity.Descripcion ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", SqlDbType.Decimal) { Value = entity.PrecioCompra });
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Decimal) { Value = entity.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("@StockActual", SqlDbType.Int) { Value = entity.StockActual });
                    cmd.Parameters.Add(new SqlParameter("@StockMinimo", SqlDbType.Int) { Value = entity.StockMinimo });
                    cmd.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = entity.CategoriaId });
                    cmd.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime) { Value = entity.FechaRegistro });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public bool Update(Producto entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"UPDATE dbo.Productos 
                                 SET Nombre = @Nombre, Descripcion = @Descripcion, PrecioCompra = @PrecioCompra,
                                     PrecioVenta = @PrecioVenta, StockActual = @StockActual, StockMinimo = @StockMinimo,
                                     CategoriaId = @CategoriaId, Activo = @Activo
                                 WHERE ProductoId = @ProductoId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = entity.ProductoId });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150) { Value = entity.Nombre });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 500) { Value = (object?)entity.Descripcion ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@PrecioCompra", SqlDbType.Decimal) { Value = entity.PrecioCompra });
                    cmd.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Decimal) { Value = entity.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("@StockActual", SqlDbType.Int) { Value = entity.StockActual });
                    cmd.Parameters.Add(new SqlParameter("@StockMinimo", SqlDbType.Int) { Value = entity.StockMinimo });
                    cmd.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = entity.CategoriaId });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            // Borrado lógico con Activo = 0
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE dbo.Productos SET Activo = 0 WHERE ProductoId = @ProductoId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateStock(int productId, int quantityChange)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE dbo.Productos SET StockActual = StockActual + @Change WHERE ProductoId = @ProductoId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Change", SqlDbType.Int) { Value = quantityChange });
                    cmd.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = productId });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Producto MapProducto(SqlDataReader reader)
        {
            var product = new Producto
            {
                ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                PrecioCompra = reader.GetDecimal(reader.GetOrdinal("PrecioCompra")),
                PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
                StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                StockMinimo = reader.GetInt32(reader.GetOrdinal("StockMinimo")),
                CategoriaId = reader.GetInt32(reader.GetOrdinal("CategoriaId")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
            };

            if (!reader.IsDBNull(reader.GetOrdinal("CategoriaNombre")))
            {
                product.Categoria = new Categoria
                {
                    CategoriaId = product.CategoriaId,
                    Nombre = reader.GetString(reader.GetOrdinal("CategoriaNombre"))
                };
            }

            return product;
        }
    }

    /// <summary>
    /// Alias de compatibilidad con ProductAdoRepository.
    /// </summary>
    public class ProductAdoRepository : ProductoAdoRepository
    {
    }
}
