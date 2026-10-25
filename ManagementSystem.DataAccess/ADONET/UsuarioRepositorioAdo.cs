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
    /// Repositorio ADO.NET para la gestión de Usuarios y autenticación.
    /// Utiliza parámetros SQL, using y cierre explícito en el bloque finally.
    /// </summary>
    public class UsuarioRepositorioAdo : IUsuarioRepositorio
    {
        public IEnumerable<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                               r.NombreRol
                                        FROM dbo.Usuarios u
                                        INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                        ORDER BY u.NombreUsuario";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearUsuario(lector));
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

        public Usuario? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                               r.NombreRol
                                        FROM dbo.Usuarios u
                                        INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                        WHERE u.UsuarioId = @UsuarioId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearUsuario(lector);
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

        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                               r.NombreRol
                                        FROM dbo.Usuarios u
                                        INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                        WHERE u.NombreUsuario = @NombreUsuario";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = nombreUsuario.Trim() });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearUsuario(lector);
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

        public Usuario? ObtenerPorEmpleadoId(int empleadoId)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                               r.NombreRol
                                        FROM dbo.Usuarios u
                                        INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                        WHERE u.EmpleadoId = @EmpleadoId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = empleadoId });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearUsuario(lector);
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

        public int Insertar(Usuario entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"INSERT INTO dbo.Usuarios (EmpleadoId, NombreUsuario, ContrasenaHash, RolId, Activo)
                                        VALUES (@EmpleadoId, @NombreUsuario, @ContrasenaHash, @RolId, @Activo);
                                        SELECT SCOPE_IDENTITY();";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = entidad.EmpleadoId });
                        comando.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = entidad.NombreUsuario });
                        comando.Parameters.Add(new SqlParameter("@ContrasenaHash", SqlDbType.VarChar, 256) { Value = entidad.ContrasenaHash });
                        comando.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = entidad.RolId });
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

        public bool Actualizar(Usuario entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Usuarios
                                        SET NombreUsuario = @NombreUsuario, RolId = @RolId, Activo = @Activo";

                    if (!string.IsNullOrWhiteSpace(entidad.ContrasenaHash))
                    {
                        consulta += ", ContrasenaHash = @ContrasenaHash";
                    }

                    consulta += " WHERE UsuarioId = @UsuarioId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = entidad.UsuarioId });
                        comando.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = entidad.NombreUsuario });
                        comando.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = entidad.RolId });
                        comando.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                        if (!string.IsNullOrWhiteSpace(entidad.ContrasenaHash))
                        {
                            comando.Parameters.Add(new SqlParameter("@ContrasenaHash", SqlDbType.VarChar, 256) { Value = entidad.ContrasenaHash });
                        }

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
                    string consulta = "UPDATE dbo.Usuarios SET Activo = 0 WHERE UsuarioId = @UsuarioId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = id });
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

        public Usuario? ValidarCredenciales(string nombreUsuario, string contrasenaHash)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                               r.NombreRol
                                        FROM dbo.Usuarios u
                                        INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                        WHERE u.NombreUsuario = @NombreUsuario AND u.ContrasenaHash = @ContrasenaHash AND u.Activo = 1";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = nombreUsuario.Trim() });
                        comando.Parameters.Add(new SqlParameter("@ContrasenaHash", SqlDbType.VarChar, 256) { Value = contrasenaHash.Trim() });

                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearUsuario(lector);
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

        private static Usuario MapearUsuario(SqlDataReader lector)
        {
            return new Usuario
            {
                UsuarioId = lector.GetInt32(lector.GetOrdinal("UsuarioId")),
                EmpleadoId = lector.GetInt32(lector.GetOrdinal("EmpleadoId")),
                NombreUsuario = lector.GetString(lector.GetOrdinal("NombreUsuario")),
                ContrasenaHash = lector.GetString(lector.GetOrdinal("ContrasenaHash")),
                RolId = lector.GetInt32(lector.GetOrdinal("RolId")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo")),
                Rol = new Rol
                {
                    RolId = lector.GetInt32(lector.GetOrdinal("RolId")),
                    NombreRol = lector.GetString(lector.GetOrdinal("NombreRol"))
                }
            };
        }
    }
}
