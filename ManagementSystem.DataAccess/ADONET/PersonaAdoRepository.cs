using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    /// <summary>
    /// Repositorio ADO.NET para la entidad base Persona.
    /// Aplica parámetros SQL, try-finally, using y mapeo con SqlDataReader.
    /// </summary>
    public class PersonaAdoRepository : IPersonaRepository, IPersonRepository
    {
        public IEnumerable<Persona> GetAll()
        {
            var list = new List<Persona>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                 FROM dbo.Persona
                                 ORDER BY NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapPersona(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Persona? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                 FROM dbo.Persona
                                 WHERE PersonaId = @PersonaId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPersona(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Persona? GetByIdentification(string identification)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                 FROM dbo.Persona
                                 WHERE Identificacion = @Identificacion";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = identification.Trim() });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPersona(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Persona> Search(string term)
        {
            var list = new List<Persona>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                 FROM dbo.Persona
                                 WHERE NombreCompleto LIKE @Term OR Identificacion LIKE @Term OR Email LIKE @Term
                                 ORDER BY NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Term", SqlDbType.VarChar, 100) { Value = $"%{term}%" });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapPersona(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Persona entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"INSERT INTO dbo.Persona (NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo)
                                 VALUES (@NombreCompleto, @Identificacion, @Email, @Telefono, @Direccion, @FechaRegistro, @Activo);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entity.NombreCompleto });
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entity.Identificacion });
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entity.Email ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entity.Telefono ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entity.Direccion ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime) { Value = entity.FechaRegistro });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(Persona entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"UPDATE dbo.Persona
                                 SET NombreCompleto = @NombreCompleto, Identificacion = @Identificacion,
                                     Email = @Email, Telefono = @Telefono, Direccion = @Direccion,
                                     Activo = @Activo
                                 WHERE PersonaId = @PersonaId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = entity.PersonaId });
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entity.NombreCompleto });
                    cmd.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entity.Identificacion });
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entity.Email ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entity.Telefono ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entity.Direccion ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE dbo.Persona SET Activo = 0 WHERE PersonaId = @PersonaId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public IEnumerable<ResumenPersonaDto> GetAllPersonSummaries()
        {
            return SearchPersonSummaries(null, null);
        }

        public IEnumerable<ResumenPersonaDto> SearchPersonSummaries(string? term, string? filterType)
        {
            var list = new List<ResumenPersonaDto>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"
                    SELECT p.PersonaId, p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.Activo,
                           c.ClienteId, c.LimiteCredito,
                           e.EmpleadoId, e.Puesto, e.Salario,
                           u.UsuarioId, u.NombreUsuario, u.RolId, r.NombreRol
                    FROM dbo.Persona p
                    LEFT JOIN dbo.Cliente c ON p.PersonaId = c.PersonaId AND c.Activo = 1
                    LEFT JOIN dbo.Empleado e ON p.PersonaId = e.PersonaId AND e.Activo = 1
                    LEFT JOIN dbo.Usuarios u ON e.EmpleadoId = u.EmpleadoId AND u.Activo = 1
                    LEFT JOIN dbo.Roles r ON u.RolId = r.RolId
                    WHERE 1 = 1";

                if (!string.IsNullOrWhiteSpace(term))
                {
                    query += " AND (p.NombreCompleto LIKE @Term OR p.Identificacion LIKE @Term OR p.Email LIKE @Term)";
                }

                if (!string.IsNullOrWhiteSpace(filterType))
                {
                    if (filterType.Equals("Clientes", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND c.ClienteId IS NOT NULL";
                    }
                    else if (filterType.Equals("Empleados", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND e.EmpleadoId IS NOT NULL";
                    }
                    else if (filterType.Equals("Usuarios", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND u.UsuarioId IS NOT NULL";
                    }
                }

                query += " ORDER BY p.NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(term))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Term", SqlDbType.VarChar, 100) { Value = $"%{term.Trim()}%" });
                    }

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dto = new ResumenPersonaDto
                            {
                                PersonaId = reader.GetInt32(reader.GetOrdinal("PersonaId")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                Identificacion = reader.GetString(reader.GetOrdinal("Identificacion")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                                Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),

                                EsCliente = !reader.IsDBNull(reader.GetOrdinal("ClienteId")),
                                ClienteId = reader.IsDBNull(reader.GetOrdinal("ClienteId")) ? null : reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                LimiteCredito = reader.IsDBNull(reader.GetOrdinal("LimiteCredito")) ? null : reader.GetDecimal(reader.GetOrdinal("LimiteCredito")),

                                EsEmpleado = !reader.IsDBNull(reader.GetOrdinal("EmpleadoId")),
                                EmpleadoId = reader.IsDBNull(reader.GetOrdinal("EmpleadoId")) ? null : reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                                Puesto = reader.IsDBNull(reader.GetOrdinal("Puesto")) ? null : reader.GetString(reader.GetOrdinal("Puesto")),
                                Salario = reader.IsDBNull(reader.GetOrdinal("Salario")) ? null : reader.GetDecimal(reader.GetOrdinal("Salario")),

                                TieneUsuario = !reader.IsDBNull(reader.GetOrdinal("UsuarioId")),
                                UsuarioId = reader.IsDBNull(reader.GetOrdinal("UsuarioId")) ? null : reader.GetInt32(reader.GetOrdinal("UsuarioId")),
                                NombreUsuario = reader.IsDBNull(reader.GetOrdinal("NombreUsuario")) ? null : reader.GetString(reader.GetOrdinal("NombreUsuario")),
                                RolId = reader.IsDBNull(reader.GetOrdinal("RolId")) ? null : reader.GetInt32(reader.GetOrdinal("RolId")),
                                NombreRol = reader.IsDBNull(reader.GetOrdinal("NombreRol")) ? null : reader.GetString(reader.GetOrdinal("NombreRol"))
                            };

                            var types = new List<string>();
                            if (dto.EsCliente) types.Add("Cliente");
                            if (dto.EsEmpleado) types.Add("Empleado");
                            if (dto.TieneUsuario) types.Add("Usuario");

                            dto.TiposPersona = types.Count > 0 ? string.Join(" / ", types) : "Persona Base";
                            list.Add(dto);
                        }
                    }
                }
            }
            return list;
        }

        private static Persona MapPersona(SqlDataReader reader)
        {
            return new Persona
            {
                PersonaId = reader.GetInt32(reader.GetOrdinal("PersonaId")),
                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                Identificacion = reader.GetString(reader.GetOrdinal("Identificacion")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con PersonAdoRepository.
    /// </summary>
    public class PersonAdoRepository : PersonaAdoRepository
    {
    }
}
