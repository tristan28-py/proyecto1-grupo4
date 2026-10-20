using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class UserAdoRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return GetAllUsers();
        }

        public User? GetById(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT u.UserId, u.EmployeeId, u.Username, u.PasswordHash, u.RoleId, u.IsActive,
                                        r.RoleName
                                 FROM Users u
                                 INNER JOIN Roles r ON u.RoleId = r.RoleId
                                 WHERE u.UserId = @UserId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUser(reader);
                        }
                    }
                }
            }
            return null;
        }

        public User? GetByUsername(string username)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT u.UserId, u.EmployeeId, u.Username, u.PasswordHash, u.RoleId, u.IsActive,
                                        r.RoleName
                                 FROM Users u
                                 INNER JOIN Roles r ON u.RoleId = r.RoleId
                                 WHERE u.Username = @Username";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username.Trim() });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUser(reader);
                        }
                    }
                }
            }
            return null;
        }

        public User? GetByEmployeeId(int employeeId)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT u.UserId, u.EmployeeId, u.Username, u.PasswordHash, u.RoleId, u.IsActive,
                                        r.RoleName
                                 FROM Users u
                                 INNER JOIN Roles r ON u.RoleId = r.RoleId
                                 WHERE u.EmployeeId = @EmployeeId AND u.IsActive = 1";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUser(reader);
                        }
                    }
                }
            }
            return null;
        }

        public int Add(User entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"INSERT INTO Users (EmployeeId, Username, PasswordHash, RoleId, IsActive)
                                 VALUES (@EmployeeId, @Username, @PasswordHash, @RoleId, @IsActive);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = entity.EmployeeId });
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = entity.Username });
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 256) { Value = entity.PasswordHash });
                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = entity.RoleId });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(User entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"UPDATE Users
                                 SET Username = @Username, RoleId = @RoleId, IsActive = @IsActive";

                if (!string.IsNullOrWhiteSpace(entity.PasswordHash))
                {
                    query += ", PasswordHash = @PasswordHash";
                }

                query += " WHERE UserId = @UserId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = entity.UserId });
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = entity.Username });
                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = entity.RoleId });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    if (!string.IsNullOrWhiteSpace(entity.PasswordHash))
                    {
                        cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 256) { Value = entity.PasswordHash });
                    }

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "UPDATE Users SET IsActive = 0 WHERE UserId = @UserId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public User? ValidateLogin(string username, string passwordHash)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT u.UserId, u.EmployeeId, u.Username, u.PasswordHash, u.RoleId, u.IsActive,
                                        r.RoleName
                                 FROM Users u
                                 INNER JOIN Roles r ON u.RoleId = r.RoleId
                                 WHERE u.Username = @Username AND u.PasswordHash = @PasswordHash AND u.IsActive = 1";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 256) { Value = passwordHash });

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUser(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var list = new List<User>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"SELECT u.UserId, u.EmployeeId, u.Username, u.PasswordHash, u.RoleId, u.IsActive,
                                        r.RoleName
                                 FROM Users u
                                 INNER JOIN Roles r ON u.RoleId = r.RoleId
                                 ORDER BY u.Username";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapUser(reader));
                        }
                    }
                }
            }
            return list;
        }

        private static User MapUser(SqlDataReader reader)
        {
            return new User
            {
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                Role = new Role
                {
                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
                }
            };
        }
    }
}