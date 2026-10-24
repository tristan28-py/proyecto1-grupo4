using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class ClienteAdoRepository : IClienteRepository, IClientRepository
    {
        public IEnumerable<Cliente> GetAll()
        {
            var list = new List<Cliente>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT c.ClienteId, c.PersonaId, c.LimiteCredito, c.FechaAfiliacion, c.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Cliente c
                                 INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                 ORDER BY p.NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapCliente(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Cliente? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT c.ClienteId, c.PersonaId, c.LimiteCredito, c.FechaAfiliacion, c.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Cliente c
                                 INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                 WHERE c.ClienteId = @ClienteId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCliente(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Cliente> SearchClients(string term)
        {
            var list = new List<Cliente>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT c.ClienteId, c.PersonaId, c.LimiteCredito, c.FechaAfiliacion, c.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Cliente c
                                 INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                 WHERE c.Activo = 1 AND (p.NombreCompleto LIKE @Term OR p.Identificacion LIKE @Term)
                                 ORDER BY p.NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Term", SqlDbType.VarChar, 100) { Value = $"%{term}%" });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapCliente(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Cliente entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        int personId = entity.PersonaId;
                        if (personId <= 0)
                        {
                            string personQuery = @"INSERT INTO dbo.Persona (NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo)
                                                   VALUES (@NombreCompleto, @Identificacion, @Email, @Telefono, @Direccion, @FechaRegistro, @Activo);
                                                   SELECT SCOPE_IDENTITY();";

                            using (var cmdPerson = new SqlCommand(personQuery, conn, trans))
                            {
                                cmdPerson.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entity.NombreCompleto });
                                cmdPerson.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entity.Identificacion });
                                cmdPerson.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entity.Email ?? DBNull.Value });
                                cmdPerson.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entity.Telefono ?? DBNull.Value });
                                cmdPerson.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entity.Direccion ?? DBNull.Value });
                                cmdPerson.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime) { Value = entity.FechaRegistro });
                                cmdPerson.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                                personId = Convert.ToInt32(cmdPerson.ExecuteScalar());
                            }
                        }

                        string clientQuery = @"INSERT INTO dbo.Cliente (PersonaId, LimiteCredito, FechaAfiliacion, Activo)
                                               VALUES (@PersonaId, @LimiteCredito, @FechaAfiliacion, @Activo);
                                               SELECT SCOPE_IDENTITY();";

                        int clientId;
                        using (var cmdClient = new SqlCommand(clientQuery, conn, trans))
                        {
                            cmdClient.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = personId });
                            cmdClient.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal) { Value = entity.LimiteCredito });
                            cmdClient.Parameters.Add(new SqlParameter("@FechaAfiliacion", SqlDbType.DateTime) { Value = entity.FechaAfiliacion });
                            cmdClient.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                            clientId = Convert.ToInt32(cmdClient.ExecuteScalar());
                        }

                        trans.Commit();
                        return clientId;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool Update(Cliente entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string personQuery = @"UPDATE dbo.Persona 
                                               SET NombreCompleto = @NombreCompleto, Identificacion = @Identificacion,
                                                   Email = @Email, Telefono = @Telefono, Direccion = @Direccion,
                                                   Activo = @Activo
                                               WHERE PersonaId = (SELECT PersonaId FROM dbo.Cliente WHERE ClienteId = @ClienteId)";

                        using (var cmdPerson = new SqlCommand(personQuery, conn, trans))
                        {
                            cmdPerson.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = entity.ClienteId });
                            cmdPerson.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entity.NombreCompleto });
                            cmdPerson.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entity.Identificacion });
                            cmdPerson.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entity.Email ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entity.Telefono ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entity.Direccion ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                            cmdPerson.ExecuteNonQuery();
                        }

                        string clientQuery = @"UPDATE dbo.Cliente 
                                               SET LimiteCredito = @LimiteCredito, Activo = @Activo
                                               WHERE ClienteId = @ClienteId";

                        using (var cmdClient = new SqlCommand(clientQuery, conn, trans))
                        {
                            cmdClient.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = entity.ClienteId });
                            cmdClient.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal) { Value = entity.LimiteCredito });
                            cmdClient.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                            cmdClient.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE dbo.Cliente SET Activo = 0 WHERE ClienteId = @ClienteId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Cliente MapCliente(SqlDataReader reader)
        {
            return new Cliente
            {
                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                PersonaId = reader.GetInt32(reader.GetOrdinal("PersonaId")),
                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                Identificacion = reader.GetString(reader.GetOrdinal("Identificacion")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                LimiteCredito = reader.GetDecimal(reader.GetOrdinal("LimiteCredito")),
                FechaAfiliacion = reader.GetDateTime(reader.GetOrdinal("FechaAfiliacion")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con ClientAdoRepository.
    /// </summary>
    public class ClientAdoRepository : ClienteAdoRepository
    {
    }
}
