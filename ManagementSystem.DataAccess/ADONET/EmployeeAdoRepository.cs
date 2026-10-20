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
    /// Repositorio ADO.NET para la entidad Employee (demuestra Herencia de Person).
    /// </summary>
    public class EmployeeAdoRepository : IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll()
        {
            var list = new List<Employee>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT e.EmployeeId, e.PersonId, e.Position, e.Salary, e.EntryDate, e.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Employees e
                                 INNER JOIN Persons p ON e.PersonId = p.PersonId
                                 ORDER BY p.FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapEmployee(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Employee? GetById(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT e.EmployeeId, e.PersonId, e.Position, e.Salary, e.EntryDate, e.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Employees e
                                 INNER JOIN Persons p ON e.PersonId = p.PersonId
                                 WHERE e.EmployeeId = @EmployeeId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmployee(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Employee? GetByPersonId(int personId)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT e.EmployeeId, e.PersonId, e.Position, e.Salary, e.EntryDate, e.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Employees e
                                 INNER JOIN Persons p ON e.PersonId = p.PersonId
                                 WHERE e.PersonId = @PersonId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = personId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmployee(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Employee> GetActiveEmployees()
        {
            var list = new List<Employee>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT e.EmployeeId, e.PersonId, e.Position, e.Salary, e.EntryDate, e.IsActive,
                                        p.FullName, p.Identification, p.Email, p.PhoneNumber, p.Address, p.RegistrationDate
                                 FROM Employees e
                                 INNER JOIN Persons p ON e.PersonId = p.PersonId
                                 WHERE e.IsActive = 1
                                 ORDER BY p.FullName";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapEmployee(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Employee entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"INSERT INTO Employees (PersonId, Position, Salary, EntryDate, IsActive)
                                 VALUES (@PersonId, @Position, @Salary, @EntryDate, @IsActive);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int) { Value = entity.PersonId });
                    cmd.Parameters.Add(new SqlParameter("@Position", SqlDbType.NVarChar, 100) { Value = entity.Position });
                    cmd.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Decimal) { Value = entity.Salary });
                    cmd.Parameters.Add(new SqlParameter("@EntryDate", SqlDbType.DateTime) { Value = entity.EntryDate });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(Employee entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"UPDATE Employees
                                 SET Position = @Position, Salary = @Salary, IsActive = @IsActive
                                 WHERE EmployeeId = @EmployeeId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = entity.EmployeeId });
                    cmd.Parameters.Add(new SqlParameter("@Position", SqlDbType.NVarChar, 100) { Value = entity.Position });
                    cmd.Parameters.Add(new SqlParameter("@Salary", SqlDbType.Decimal) { Value = entity.Salary });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "UPDATE Employees SET IsActive = 0 WHERE EmployeeId = @EmployeeId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Employee MapEmployee(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                Position = reader.GetString(reader.GetOrdinal("Position")),
                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                EntryDate = reader.GetDateTime(reader.GetOrdinal("EntryDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Identification = reader.GetString(reader.GetOrdinal("Identification")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
            };
        }
    }
}