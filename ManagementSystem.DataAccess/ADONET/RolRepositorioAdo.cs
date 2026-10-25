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
    /// Repositorio ADO.NET para la gestión de Roles de usuario.
    /// Implementa IRolRepositorio (y por herencia IRepositorio&lt;Rol&gt;).
    /// </summary>
    public class RolRepositorioAdo : IRolRepositorio
    {
        public IEnumerable<Rol> ObtenerTodos()
        {
            var lista = new List<Rol>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "SELECT RolId, NombreRol, Descripcion, Activo FROM dbo.Roles ORDER BY RolId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearRol(lector));
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

        public IEnumerable<Rol> ObtenerRolesActivos()
        {
            var lista = new List<Rol>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "SELECT RolId, NombreRol, Descripcion, Activo FROM dbo.Roles WHERE Activo = 1 ORDER BY RolId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearRol(lector));
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

        public Rol? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "SELECT RolId, NombreRol, Descripcion, Activo FROM dbo.Roles WHERE RolId = @RolId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearRol(lector);
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

        public int Insertar(Rol entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"INSERT INTO dbo.Roles (NombreRol, Descripcion, Activo)
                                        VALUES (@NombreRol, @Descripcion, @Activo);
                                        SELECT SCOPE_IDENTITY();";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@NombreRol", SqlDbType.VarChar, 50) { Value = entidad.NombreRol });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 200) { Value = (object?)entidad.Descripcion ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                        conexion.Open();
                        return Convert.ToInt32(comando.ExecuteScalar());
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

        public bool Actualizar(Rol entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Roles
                                        SET NombreRol = @NombreRol, Descripcion = @Descripcion, Activo = @Activo
                                        WHERE RolId = @RolId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = entidad.RolId });
                        comando.Parameters.Add(new SqlParameter("@NombreRol", SqlDbType.VarChar, 50) { Value = entidad.NombreRol });
                        comando.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 200) { Value = (object?)entidad.Descripcion ?? DBNull.Value });
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
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Roles SET Activo = 0 WHERE RolId = @RolId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = id });
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

        private static Rol MapearRol(SqlDataReader lector)
        {
            return new Rol
            {
                RolId = lector.GetInt32(lector.GetOrdinal("RolId")),
                NombreRol = lector.GetString(lector.GetOrdinal("NombreRol")),
                Descripcion = lector.IsDBNull(lector.GetOrdinal("Descripcion")) ? null : lector.GetString(lector.GetOrdinal("Descripcion")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo"))
            };
        }
    }
}
