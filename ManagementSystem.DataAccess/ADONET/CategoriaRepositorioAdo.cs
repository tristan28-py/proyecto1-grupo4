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
    /// Repositorio ADO.NET para la gestión de Categorías.
    /// Implementa ICategoriaRepositorio (y por herencia IRepositorio&lt;Categoria&gt;) aplicando using y cierre en finally.
    /// </summary>
    public class CategoriaRepositorioAdo : ICategoriaRepositorio
    {
        public IEnumerable<Categoria> ObtenerTodos()
        {
            var lista = new List<Categoria>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "SELECT CategoriaId, Nombre, Descripcion, FechaCreacion, Activo FROM dbo.Categorias ORDER BY Nombre";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearCategoria(lector));
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

        public IEnumerable<Categoria> ObtenerActivas()
        {
            var lista = new List<Categoria>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "SELECT CategoriaId, Nombre, Descripcion, FechaCreacion, Activo FROM dbo.Categorias WHERE Activo = 1 ORDER BY Nombre";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearCategoria(lector));
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

        public Categoria? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "SELECT CategoriaId, Nombre, Descripcion, FechaCreacion, Activo FROM dbo.Categorias WHERE CategoriaId = @CategoriaId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearCategoria(lector);
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

        public int Insertar(Categoria entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"INSERT INTO dbo.Categorias (Nombre, Descripcion, FechaCreacion, Activo) 
                                        VALUES (@Nombre, @Descripcion, @FechaCreacion, @Activo);
                                        SELECT SCOPE_IDENTITY();";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = entidad.Nombre });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 255) { Value = (object?)entidad.Descripcion ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@FechaCreacion", SqlDbType.DateTime) { Value = entidad.FechaCreacion });
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

        public bool Actualizar(Categoria entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Categorias 
                                        SET Nombre = @Nombre, Descripcion = @Descripcion, Activo = @Activo 
                                        WHERE CategoriaId = @CategoriaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = entidad.CategoriaId });
                        comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = entidad.Nombre });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 255) { Value = (object?)entidad.Descripcion ?? DBNull.Value });
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
            // Eliminación lógica exigida en la rúbrica (conservar registros utilizando el campo Activo)
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Categorias SET Activo = 0 WHERE CategoriaId = @CategoriaId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = id });
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

        private static Categoria MapearCategoria(SqlDataReader lector)
        {
            return new Categoria
            {
                CategoriaId = lector.GetInt32(lector.GetOrdinal("CategoriaId")),
                Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? null : lector.GetString(lector.GetOrdinal("Descripcion")),
                FechaCreacion = lector.GetDateTime(lector.GetOrdinal("FechaCreacion")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo"))
            };
        }
    }
}
