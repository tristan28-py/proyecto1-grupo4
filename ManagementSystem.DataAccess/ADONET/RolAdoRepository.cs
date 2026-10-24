using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class RolAdoRepository : IRolRepository, IRoleRepository
    {
        public IEnumerable<Rol> GetAllRoles()
        {
            var list = new List<Rol>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT RolId, NombreRol, Descripcion, Activo FROM dbo.Roles WHERE Activo = 1 ORDER BY RolId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapRol(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Rol? GetById(int roleId)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT RolId, NombreRol, Descripcion, Activo FROM dbo.Roles WHERE RolId = @RolId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RolId", SqlDbType.Int) { Value = roleId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapRol(reader);
                        }
                    }
                }
            }
            return null;
        }

        private static Rol MapRol(SqlDataReader reader)
        {
            return new Rol
            {
                RolId = reader.GetInt32(reader.GetOrdinal("RolId")),
                NombreRol = reader.GetString(reader.GetOrdinal("NombreRol")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con RoleAdoRepository.
    /// </summary>
    public class RoleAdoRepository : RolAdoRepository
    {
    }
}
