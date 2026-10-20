using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class CategoryAdoRepositoty : ICategoryRepository
    {
        public IEnumerable<Category> GetAll()
        {
            var list = new List<Category>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT CategoryId, Name, Description, CreationDate, IsActive FROM Categories ORDER BY Name";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapCategory(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Category> GetActiveCategories()
        {
            var list = new List<Category>();
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT CategoryId, Name, Description, CreationDate, IsActive FROM Categories WHERE IsActive = 1 ORDER BY Name";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapCategory(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Category? GetById(int id)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "SELECT CategoryId, Name, Description, CreationDate, IsActive FROM Categories WHERE CategoryId = @CategoryId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCategory(reader);
                        }
                    }
                }
            }
            return null;
        }

        public int Add(Category entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"INSERT INTO Categories (Name, Description, CreationDate, IsActive) 
                                 VALUES (@Name, @Description, @CreationDate, @IsActive);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = entity.Name });
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 250) { Value = (object?)entity.Description ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = entity.CreationDate });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public bool Update(Category entity)
        {
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = @"UPDATE Categories 
                                 SET Name = @Name, Description = @Description, IsActive = @IsActive 
                                 WHERE CategoryId = @CategoryId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = entity.CategoryId });
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = entity.Name });
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 250) { Value = (object?)entity.Description ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = entity.IsActive });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            // Borrado lógico exigido en la rúbrica (conservar registros con campo Activo)
            using (var conn = DatabaseConnection.CreateConnection())
            {
                string query = "UPDATE Categories SET IsActive = 0 WHERE CategoryId = @CategoryId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Category MapCategory(SqlDataReader reader)
        {
            return new Category
            {
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}