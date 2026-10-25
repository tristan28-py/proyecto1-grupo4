using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    /// <summary>
    /// Repositorio ADO.NET para la gestión de Productos.
    /// Utiliza SqlConnection, SqlCommand, SqlParameter, SqlDataReader, using y cierre explícito en finally.
    /// </summary>
    public class ProductoRepositorioAdo : IProductoRepositorio
    {
        public IEnumerable<Producto> ObtenerTodos()
        {
            var lista = new List<Producto>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                               p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                               c.Nombre AS CategoriaNombre
                                        FROM dbo.Productos p
                                        INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                        ORDER BY p.Nombre";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearProducto(lector));
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return lista;
        }

        public IEnumerable<Producto> ObtenerActivos()
        {
            var lista = new List<Producto>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                               p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                               c.Nombre AS CategoriaNombre
                                        FROM dbo.Productos p
                                        INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                        WHERE p.Activo = 1
                                        ORDER BY p.Nombre";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearProducto(lector));
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return lista;
        }

        public Producto? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                               p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                               c.Nombre AS CategoriaNombre
                                        FROM dbo.Productos p
                                        INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                        WHERE p.ProductoId = @ProductoId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearProducto(lector);
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return null;
        }

        public IEnumerable<Producto> BuscarProductos(string terminoBusqueda)
        {
            var lista = new List<Producto>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                               p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                               c.Nombre AS CategoriaNombre
                                        FROM dbo.Productos p
                                        INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                        WHERE p.Activo = 1 AND (p.Nombre LIKE @Busqueda OR p.Descripcion LIKE @Busqueda)
                                        ORDER BY p.Nombre";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Busqueda", SqlDbType.VarChar, 100) { Value = $"%{terminoBusqueda}%" });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearProducto(lector));
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return lista;
        }

        public IEnumerable<Producto> ObtenerPorCategoria(int categoriaId)
        {
            var lista = new List<Producto>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT p.ProductoId, p.Nombre, p.Descripcion, p.PrecioCompra, p.PrecioVenta, 
                                               p.StockActual, p.StockMinimo, p.CategoriaId, p.FechaRegistro, p.Activo,
                                               c.Nombre AS CategoriaNombre
                                        FROM dbo.Productos p
                                        INNER JOIN dbo.Categorias c ON p.CategoriaId = c.CategoriaId
                                        WHERE p.CategoriaId = @CategoriaId AND p.Activo = 1
                                        ORDER BY p.Nombre";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = categoriaId });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearProducto(lector));
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return lista;
        }

        public int Insertar(Producto entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"INSERT INTO dbo.Productos 
                                        (Nombre, Descripcion, PrecioCompra, PrecioVenta, StockActual, StockMinimo, CategoriaId, FechaRegistro, Activo)
                                        VALUES 
                                        (@Nombre, @Descripcion, @PrecioCompra, @PrecioVenta, @StockActual, @StockMinimo, @CategoriaId, @FechaRegistro, @Activo);
                                        SELECT SCOPE_IDENTITY();";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150) { Value = entidad.Nombre });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 500) { Value = (object?)entidad.Descripcion ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@PrecioCompra", SqlDbType.Decimal) { Value = entidad.PrecioCompra });
                        comando.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Decimal) { Value = entidad.PrecioVenta });
                        comando.Parameters.Add(new SqlParameter("@StockActual", SqlDbType.Int) { Value = entidad.StockActual });
                        comando.Parameters.Add(new SqlParameter("@StockMinimo", SqlDbType.Int) { Value = entidad.StockMinimo });
                        comando.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = entidad.CategoriaId });
                        comando.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime) { Value = entidad.FechaRegistro });
                        comando.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                        conexion.Open();
                        object resultado = comando.ExecuteScalar();
                        return Convert.ToInt32(resultado);
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public bool Actualizar(Producto entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Productos 
                                        SET Nombre = @Nombre, Descripcion = @Descripcion, PrecioCompra = @PrecioCompra,
                                            PrecioVenta = @PrecioVenta, StockActual = @StockActual, StockMinimo = @StockMinimo,
                                            CategoriaId = @CategoriaId, Activo = @Activo
                                        WHERE ProductoId = @ProductoId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = entidad.ProductoId });
                        comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150) { Value = entidad.Nombre });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 500) { Value = (object?)entidad.Descripcion ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@PrecioCompra", SqlDbType.Decimal) { Value = entidad.PrecioCompra });
                        comando.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Decimal) { Value = entidad.PrecioVenta });
                        comando.Parameters.Add(new SqlParameter("@StockActual", SqlDbType.Int) { Value = entidad.StockActual });
                        comando.Parameters.Add(new SqlParameter("@StockMinimo", SqlDbType.Int) { Value = entidad.StockMinimo });
                        comando.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = entidad.CategoriaId });
                        comando.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                        conexion.Open();
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public bool Eliminar(int id)
        {
            // Eliminación lógica conservando el registro con Activo = 0
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Productos SET Activo = 0 WHERE ProductoId = @ProductoId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public bool ActualizarStock(int productoId, int cambioCantidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Productos SET StockActual = StockActual + @Cambio WHERE ProductoId = @ProductoId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Cambio", SqlDbType.Int) { Value = cambioCantidad });
                        comando.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = productoId });
                        conexion.Open();
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        private static Producto MapearProducto(SqlDataReader lector)
        {
            var producto = new Producto
            {
                ProductoId = lector.GetInt32(lector.GetOrdinal("ProductoId")),
                Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? null : lector.GetString(lector.GetOrdinal("Descripcion")),
                PrecioCompra = lector.GetDecimal(lector.GetOrdinal("PrecioCompra")),
                PrecioVenta = lector.GetDecimal(lector.GetOrdinal("PrecioVenta")),
                StockActual = lector.GetInt32(lector.GetOrdinal("StockActual")),
                StockMinimo = lector.GetInt32(lector.GetOrdinal("StockMinimo")),
                CategoriaId = lector.GetInt32(lector.GetOrdinal("CategoriaId")),
                FechaRegistro = lector.GetDateTime(lector.GetOrdinal("FechaRegistro")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo"))
            };

            if (!lector.IsDBNull(lector.GetOrdinal("CategoriaNombre")))
            {
                producto.Categoria = new Categoria
                {
                    CategoriaId = producto.CategoriaId,
                    Nombre = lector.GetString(lector.GetOrdinal("CategoriaNombre"))
                };
            }

            return producto;
        }
    }
}
