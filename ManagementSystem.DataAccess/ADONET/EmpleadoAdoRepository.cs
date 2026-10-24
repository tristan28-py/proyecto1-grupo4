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
    /// Repositorio ADO.NET para la entidad Empleado (demuestra Herencia de Persona).
    /// </summary>
    public class EmpleadoAdoRepository : IEmpleadoRepository, IEmployeeRepository
    {
        public IEnumerable<Empleado> GetAll()
        {
            var list = new List<Empleado>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Empleado e
                                 INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                 ORDER BY p.NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapEmpleado(reader));
                        }
                    }
                }
            }
            return list;
        }

        public Empleado? GetById(int id)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Empleado e
                                 INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                 WHERE e.EmpleadoId = @EmpleadoId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmpleado(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Empleado? GetByPersonId(int personId)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Empleado e
                                 INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                 WHERE e.PersonaId = @PersonaId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = personId });
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapEmpleado(reader);
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Empleado> GetActiveEmployees()
        {
            var list = new List<Empleado>();
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                        p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                 FROM dbo.Empleado e
                                 INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                 WHERE e.Activo = 1
                                 ORDER BY p.NombreCompleto";

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapEmpleado(reader));
                        }
                    }
                }
            }
            return list;
        }

        public int Add(Empleado entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"INSERT INTO dbo.Empleado (PersonaId, Puesto, Salario, FechaIngreso, Activo)
                                 VALUES (@PersonaId, @Puesto, @Salario, @FechaIngreso, @Activo);
                                 SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = entity.PersonaId });
                    cmd.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar, 100) { Value = entity.Puesto });
                    cmd.Parameters.Add(new SqlParameter("@Salario", SqlDbType.Decimal) { Value = entity.Salario });
                    cmd.Parameters.Add(new SqlParameter("@FechaIngreso", SqlDbType.DateTime) { Value = entity.FechaIngreso });
                    cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entity.Activo });

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(Empleado entity)
        {
            using (var conn = DatabaseConfig.CreateConnection())
            {
                string query = @"UPDATE dbo.Empleado
                                 SET Puesto = @Puesto, Salario = @Salario, Activo = @Activo
                                 WHERE EmpleadoId = @EmpleadoId";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = entity.EmpleadoId });
                    cmd.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar, 100) { Value = entity.Puesto });
                    cmd.Parameters.Add(new SqlParameter("@Salario", SqlDbType.Decimal) { Value = entity.Salario });
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
                string query = "UPDATE dbo.Empleado SET Activo = 0 WHERE EmpleadoId = @EmpleadoId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = id });
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Empleado MapEmpleado(SqlDataReader reader)
        {
            return new Empleado
            {
                EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                PersonaId = reader.GetInt32(reader.GetOrdinal("PersonaId")),
                Puesto = reader.GetString(reader.GetOrdinal("Puesto")),
                Salario = reader.GetDecimal(reader.GetOrdinal("Salario")),
                FechaIngreso = reader.GetDateTime(reader.GetOrdinal("FechaIngreso")),
                Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),

                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                Identificacion = reader.GetString(reader.GetOrdinal("Identificacion")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))
            };
        }
    }

    /// <summary>
    /// Alias de compatibilidad con EmployeeAdoRepository.
    /// </summary>
    public class EmployeeAdoRepository : EmpleadoAdoRepository
    {
    }
}
