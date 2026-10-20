using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class RoleAdoRepository : IRoleRepository
    {
        public IEnumerable<Role> GetAllRoles()
        {
            var list = new List<Role>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT RoleId, RoleName, Description, IsActive FROM Roles WHERE IsActive = 1 ORDER BY RoleId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Role
                            {
                                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            });
                        }
                    }
                }
            }
            return list;
        }

        public Role? GetById(int roleId)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT RoleId, RoleName, Description, IsActive FROM Roles WHERE RoleId = @RoleId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Role
                            {
                                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
