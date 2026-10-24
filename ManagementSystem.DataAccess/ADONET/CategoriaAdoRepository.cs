using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ManagementSystem.DataAccess.Connection;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.ADONET
{
    public class CategoriaAdoRepository : ICategoriaRepository, ICategoryRepository
    {
        public IEnumerable<Categoria> GetAll()
        {
            var list = new List<Categoria>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT CategoriaId, Nombre, Descripcion, FechaCreacion, Activo FROM dbo.Categorias ORDER BY Nombre";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapCategoria(reader));
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Categoria> GetActiveCategories()
        {
            var list = new List<Categoria>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT CategoriaId, Nombre, Descripcion, FechaCreacion, Activo FROM dbo.Categorias WHERE Activo = 1 ORDER BY Nombre";
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapCategoria(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Categoria? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "SELECT CategoriaId, Nombre, Descripcion, FechaCreacion, Activo FROM dbo.Categorias WHERE CategoriaId = @CategoriaId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCategoria(reader);
                        }
                    }
                }
            }
            return null;
        }

        public int Add(Categoria entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"INSERT INTO dbo.Categorias (Nombre, Descripcion, FechaCreacion, Activo) 
                                 VALUES (@Nombre, @Descripcion, @FechaCreacion, @Activo);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = entity.Nombre });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 255) { Value = (object?)entity.Descripcion ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", SqlDbType.DateTime) { Value = entity.FechaCreacion });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public bool Update(Categoria entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"UPDATE dbo.Categorias 
                                 SET Nombre = @Nombre, Descripcion = @Descripcion, Activo = @Activo 
                                 WHERE CategoriaId = @CategoriaId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = entity.CategoriaId });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = entity.Nombre });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, 255) { Value = (object?)entity.Descripcion ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            // Borrado lógico exigido en la rúbrica (conservar registros con campo Activo)
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = "UPDATE dbo.Categorias SET Activo = 0 WHERE CategoriaId = @CategoriaId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@CategoriaId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Categoria MapCategoria(SqlDataReader reader)
        {
            return new Categoria
            {
                CategoriaId = reader.GetInt32(reader.GetOrdinal("CategoriaId")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con CategoryAdoRepository.
    /// </summary>
    public class CategoryAdoRepository : CategoriaAdoRepository
    {
    }

    /// <summary>
    /// Alias de corrección por tipografía previa.
    /// </summary>
    public class CategoryAdoRepositoty : CategoriaAdoRepository
    {
    }
}
