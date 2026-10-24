using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class UsuarioAdoRepository : IUsuarioRepository, IUserRepository
    {
        public IEnumerable<Usuario> GetAll()
        {
            return GetAllUsers();
        }

        public Usuario? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                        r.NombreRol
                                 FROM dbo.Usuarios u
                                 INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                 WHERE u.UsuarioId = @UsuarioId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUsuario(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Usuario? GetByUsername(string username)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                        r.NombreRol
                                 FROM dbo.Usuarios u
                                 INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                 WHERE u.NombreUsuario = @NombreUsuario";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = username.Trim() });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUsuario(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Usuario? GetByEmployeeId(int employeeId)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                        r.NombreRol
                                 FROM dbo.Usuarios u
                                 INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                 WHERE u.EmpleadoId = @EmpleadoId AND u.Activo = 1";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = employeeId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUsuario(reader);
                        }
                    }
                }
            }
            return null;
        }

        public int Add(Usuario entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"INSERT INTO dbo.Usuarios (EmpleadoId, NombreUsuario, ContrasenaHash, RolId, Activo)
                                 VALUES (@EmpleadoId, @NombreUsuario, @ContrasenaHash, @RolId, @Activo);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = entity.EmpleadoId });
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = entity.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("@ContrasenaHash", SqlDbType.VarChar, 256) { Value = entity.ContrasenaHash });
                    cmd.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = entity.RolId });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(Usuario entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"UPDATE dbo.Usuarios
                                 SET NombreUsuario = @NombreUsuario, RolId = @RolId, Activo = @Activo";

                if (!string.IsNullOrWhiteSpace(entity.ContrasenaHash))
                {
                    query += ", ContrasenaHash = @ContrasenaHash";
                }

                query += " WHERE UsuarioId = @UsuarioId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = entity.UsuarioId });
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = entity.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = entity.RolId });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    if (!string.IsNullOrWhiteSpace(entity.ContrasenaHash))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ContrasenaHash", SqlDbType.VarChar, 256) { Value = entity.ContrasenaHash });
                    }

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE dbo.Usuarios SET Activo = 0 WHERE UsuarioId = @UsuarioId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Usuario? ValidateLogin(string username, string passwordHash)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                        r.NombreRol
                                 FROM dbo.Usuarios u
                                 INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                 WHERE u.NombreUsuario = @NombreUsuario AND u.ContrasenaHash = @ContrasenaHash AND u.Activo = 1";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 50) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@ContrasenaHash", SqlDbType.VarChar, 256) { Value = passwordHash });

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUsuario(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Usuario> GetAllUsers()
        {
            var list = new List<Usuario>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT u.UsuarioId, u.EmpleadoId, u.NombreUsuario, u.ContrasenaHash, u.RolId, u.Activo,
                                        r.NombreRol
                                 FROM dbo.Usuarios u
                                 INNER JOIN dbo.Roles r ON u.RolId = r.RolId
                                 ORDER BY u.NombreUsuario";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapUsuario(reader));
                        }
                    }
                }
            }
            return list;
        }

        private static Usuario MapUsuario(SqlDataReader reader)
        {
            return new Usuario
            {
                UsuarioId = reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
                ContrasenaHash = reader.GetString(reader.GetOrdinal("ContrasenaHash")),
                RolId = reader.GetInt32(reader.GetOrdinal("RolId")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                Rol = new Rol
                {
                    RolId = reader.GetInt32(reader.GetOrdinal("RolId")),
                    NombreRol = reader.GetString(reader.GetOrdinal("NombreRol"))
                }
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con UserAdoRepository.
    /// </summary>
    public class UserAdoRepository : UsuarioAdoRepository
    {
    }
}
