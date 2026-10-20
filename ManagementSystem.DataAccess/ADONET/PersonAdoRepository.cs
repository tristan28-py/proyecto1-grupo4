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
    /// Repositorio ADO.NET para la entidad base Person.
    /// Aplica parámetros SQL, try-finally, using y mapeo con SqlDataReader.
    /// </summary>
    public class PersonAdoRepository : IPersonRepository
    {
        public IEnumerable<Person> GetAll()
        {
            var list = new List<Person>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonId, FullName, Identification, Email, PhoneNumber, Address, RegistrationDate, IsActive
                                 FROM Persons
                                 ORDER BY FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapPerson(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Person? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonId, FullName, Identification, Email, PhoneNumber, Address, RegistrationDate, IsActive
                                 FROM Persons
                                 WHERE PersonId = @PersonId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPerson(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Person? GetByIdentification(string identification)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonId, FullName, Identification, Email, PhoneNumber, Address, RegistrationDate, IsActive
                                 FROM Persons
                                 WHERE Identification = @Identification";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Identification", SqlDbType.NVarChar, 30) { Value = identification.Trim() });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapPerson(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Person> Search(string term)
        {
            var list = new List<Person>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT PersonId, FullName, Identification, Email, PhoneNumber, Address, RegistrationDate, IsActive
                                 FROM Persons
                                 WHERE FullName LIKE @Term OR Identification LIKE @Term OR Email LIKE @Term
                                 ORDER BY FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Term", SqlDbType.NVarChar, 100) { Value = $"%{term}%" });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapPerson(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Person entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"INSERT INTO Persons (FullName, Identification, Email, PhoneNumber, Address, RegistrationDate, IsActive)
                                 VALUES (@FullName, @Identification, @Email, @PhoneNumber, @Address, @RegistrationDate, @IsActive);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 150) { Value = entity.FullName });
                    cmd.Parameters.Add(new SqlParameter("@Identification", SqlDbType.NVarChar, 30) { Value = entity.Identification });
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object?)entity.Email ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 25) { Value = (object?)entity.PhoneNumber ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = (object?)entity.Address ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@RegistrationDate", SqlDbType.DateTime) { Value = entity.RegistrationDate });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(Person entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"UPDATE Persons
                                 SET FullName = @FullName, Identification = @Identification,
                                     Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address,
                                     IsActive = @IsActive
                                 WHERE PersonId = @PersonId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = entity.PersonId });
                    cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 150) { Value = entity.FullName });
                    cmd.Parameters.Add(new SqlParameter("@Identification", SqlDbType.NVarChar, 30) { Value = entity.Identification });
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object?)entity.Email ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 25) { Value = (object?)entity.PhoneNumber ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = (object?)entity.Address ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE Persons SET IsActive = 0 WHERE PersonId = @PersonId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public IEnumerable<PersonSummaryDto> GetAllPersonSummaries()
        {
            return SearchPersonSummaries(null, null);
        }

        public IEnumerable<PersonSummaryDto> SearchPersonSummaries(string? term, string? filterType)
        {
            var list = new List<PersonSummaryDto>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"
                    SELECT p.PersonId, p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.IsActive,
                           c.ClientId, c.CreditLimit,
                           e.EmployeeId, e.Position, e.Salary,
                           u.UserId, u.Username, u.RoleId, r.RoleName
                    FROM Persons p
                    LEFT JOIN Clients c ON p.PersonId = c.PersonId AND c.IsActive = 1
                    LEFT JOIN Employees e ON p.PersonId = e.PersonId AND e.IsActive = 1
                    LEFT JOIN Users u ON e.EmployeeId = u.EmployeeId AND u.IsActive = 1
                    LEFT JOIN Roles r ON u.RoleId = r.RoleId
                    WHERE 1 = 1";

                if (!string.IsNullOrWhiteSpace(term))
                {
                    query += " AND (p.FullName LIKE @Term OR p.Identification LIKE @Term OR p.Email LIKE @Term)";
                }

                if (!string.IsNullOrWhiteSpace(filterType))
                {
                    if (filterType.Equals("Clientes", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND c.ClientId IS NOT NULL";
                    }
                    else if (filterType.Equals("Empleados", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND e.EmployeeId IS NOT NULL";
                    }
                    else if (filterType.Equals("Usuarios", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND u.UserId IS NOT NULL";
                    }
                }

                query += " ORDER BY p.FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(term))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Term", SqlDbType.NVarChar, 100) { Value = $"%{term.Trim()}%" });
                    }

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dto = new PersonSummaryDto
                            {
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                Identification = reader.GetString(reader.GetOrdinal("Identification")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                                IsClient = !reader.IsDBNull(reader.GetOrdinal("ClientId")),
                                ClientId = reader.IsDBNull(reader.GetOrdinal("ClientId")) ? null : reader.GetInt32(reader.GetOrdinal("ClientId")),
                                CreditLimit = reader.IsDBNull(reader.GetOrdinal("CreditLimit")) ? null : reader.GetDecimal(reader.GetOrdinal("CreditLimit")),

                                IsEmployee = !reader.IsDBNull(reader.GetOrdinal("EmployeeId")),
                                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeId")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                Position = reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString(reader.GetOrdinal("Position")),
                                Salary = reader.IsDBNull(reader.GetOrdinal("Salary")) ? null : reader.GetDecimal(reader.GetOrdinal("Salary")),

                                HasUser = !reader.IsDBNull(reader.GetOrdinal("UserId")),
                                UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? null : reader.GetInt32(reader.GetOrdinal("UserId")),
                                Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? null : reader.GetString(reader.GetOrdinal("Username")),
                                RoleId = reader.IsDBNull(reader.GetOrdinal("RoleId")) ? null : reader.GetInt32(reader.GetOrdinal("RoleId")),
                                RoleName = reader.IsDBNull(reader.GetOrdinal("RoleName")) ? null : reader.GetString(reader.GetOrdinal("RoleName"))
                            };

                            var types = new List<string>();
                            if (dto.IsClient) types.Add("Cliente");
                            if (dto.IsEmployee) types.Add("Empleado");
                            if (dto.HasUser) types.Add("Usuario");

                            dto.PersonTypes = types.Count > 0 ? string.Join(" / ", types) : "Persona Base";
                            list.Add(dto);
                        }
                    }
                }
            }
            return list;
        }

        private static Person MapPerson(SqlDataReader reader)
        {
            return new Person
            {
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Identification = reader.GetString(reader.GetOrdinal("Identification")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}