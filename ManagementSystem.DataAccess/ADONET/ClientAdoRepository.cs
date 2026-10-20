using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class ClientAdoRepository : IClientRepository
    {
        public IEnumerable<Client> GetAll()
        {
            var list = new List<Client>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT c.ClientId, c.PersonId, c.CreditLimit, c.AffiliationDate, c.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Clients c
                                 INNER JOIN Persons p ON c.PersonId = p.PersonId
                                 ORDER BY p.FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapClient(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Client? GetById(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT c.ClientId, c.PersonId, c.CreditLimit, c.AffiliationDate, c.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Clients c
                                 INNER JOIN Persons p ON c.PersonId = p.PersonId
                                 WHERE c.ClientId = @ClientId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClient(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Client> SearchClients(string term)
        {
            var list = new List<Client>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT c.ClientId, c.PersonId, c.CreditLimit, c.AffiliationDate, c.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Clients c
                                 INNER JOIN Persons p ON c.PersonId = p.PersonId
                                 WHERE c.IsActive = 1 AND (p.FullName LIKE @Term OR p.Identification LIKE @Term)
                                 ORDER BY p.FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Term", SqlDbType.NVarChar, 100) { Value = $"%{term}%" });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapClient(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Client entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar en tabla Persons
                        string personQuery = @"INSERT INTO Persons (FullName, Identification, Email, PhoneNumber, Address, RegistrationDate, IsActive)
                                               VALUES (@FullName, @Identification, @Email, @PhoneNumber, @Address, @RegistrationDate, @IsActive);
                                               SELECT SCOPE_IDENTITY();";

                        int personId;
                        using (var cmdPerson = new SqlCommand(personQuery, conn, trans))
                        {
                            cmdPerson.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 150) { Value = entity.FullName });
                            cmdPerson.Parameters.Add(new SqlParameter("@Identification", SqlDbType.NVarChar, 30) { Value = entity.Identification });
                            cmdPerson.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object?)entity.Email ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 25) { Value = (object?)entity.PhoneNumber ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = (object?)entity.Address ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@RegistrationDate", SqlDbType.DateTime) { Value = entity.RegistrationDate });
                            cmdPerson.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                            personId = Convert.ToInt32(cmdPerson.ExecuteScalar());
                        }

                        // 2. Insertar en tabla Clients
                        string clientQuery = @"INSERT INTO Clients (PersonId, CreditLimit, AffiliationDate, IsActive)
                                               VALUES (@PersonId, @CreditLimit, @AffiliationDate, @IsActive);
                                               SELECT SCOPE_IDENTITY();";

                        int clientId;
                        using (var cmdClient = new SqlCommand(clientQuery, conn, trans))
                        {
                            cmdClient.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = personId });
                            cmdClient.Parameters.Add(new SqlParameter("@CreditLimit", SqlDbType.Decimal) { Value = entity.CreditLimit });
                            cmdClient.Parameters.Add(new SqlParameter("@AffiliationDate", SqlDbType.DateTime) { Value = entity.AffiliationDate });
                            cmdClient.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

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

        public bool Update(Client entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string personQuery = @"UPDATE Persons 
                                               SET FullName = @FullName, Identification = @Identification,
                                                   Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address,
                                                   IsActive = @IsActive
                                               WHERE PersonId = (SELECT PersonId FROM Clients WHERE ClientId = @ClientId)";

                        using (var cmdPerson = new SqlCommand(personQuery, conn, trans))
                        {
                            cmdPerson.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = entity.ClientId });
                            cmdPerson.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 150) { Value = entity.FullName });
                            cmdPerson.Parameters.Add(new SqlParameter("@Identification", SqlDbType.NVarChar, 30) { Value = entity.Identification });
                            cmdPerson.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object?)entity.Email ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 25) { Value = (object?)entity.PhoneNumber ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = (object?)entity.Address ?? DBNull.Value });
                            cmdPerson.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                            cmdPerson.ExecuteNonQuery();
                        }

                        string clientQuery = @"UPDATE Clients 
                                               SET CreditLimit = @CreditLimit, IsActive = @IsActive
                                               WHERE ClientId = @ClientId";

                        using (var cmdClient = new SqlCommand(clientQuery, conn, trans))
                        {
                            cmdClient.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = entity.ClientId });
                            cmdClient.Parameters.Add(new SqlParameter("@CreditLimit", SqlDbType.Decimal) { Value = entity.CreditLimit });
                            cmdClient.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

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
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "UPDATE Clients SET IsActive = 0 WHERE ClientId = @ClientId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Client MapClient(SqlDataReader reader)
        {
            return new Client
            {
                ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Identification = reader.GetString(reader.GetOrdinal("Identification")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                CreditLimit = reader.GetDecimal(reader.GetOrdinal("CreditLimit")),
                AffiliationDate = reader.GetDateTime(reader.GetOrdinal("AffiliationDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}