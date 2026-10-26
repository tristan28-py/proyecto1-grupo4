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
    /// Acceso a datos mediante ADO.NET para la tabla de empleados y sus datos personales asociados.
    /// </summary>
    public class EmpleadoRepositorioAdo : IEmpleadoRepositorio
    {
        public IEnumerable<Empleado> ObtenerTodos()
        {
            var lista = new List<Empleado>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Empleado e
                                        INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                        ORDER BY p.NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearEmpleado(lector));
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return lista;
        }

        public Empleado? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Empleado e
                                        INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                        WHERE e.EmpleadoId = @EmpleadoId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearEmpleado(lector);
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return null;
        }

        public Empleado? ObtenerPorPersonaId(int personaId)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Empleado e
                                        INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                        WHERE e.PersonaId = @PersonaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = personaId });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearEmpleado(lector);
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return null;
        }

        public IEnumerable<Empleado> ObtenerEmpleadosActivos()
        {
            var lista = new List<Empleado>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT e.EmpleadoId, e.PersonaId, e.Puesto, e.Salario, e.FechaIngreso, e.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Empleado e
                                        INNER JOIN dbo.Persona p ON e.PersonaId = p.PersonaId
                                        WHERE e.Activo = 1
                                        ORDER BY p.NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearEmpleado(lector));
                            }
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
            return lista;
        }

        public int Insertar(Empleado entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    conexion.Open();
                    using (var transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            int personaId = entidad.PersonaId;
                            if (personaId <= 0)
                            {
                                string consultaPersona = @"INSERT INTO dbo.Persona (NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo)
                                                           VALUES (@NombreCompleto, @Identificacion, @Email, @Telefono, @Direccion, @FechaRegistro, @Activo);
                                                           SELECT SCOPE_IDENTITY();";

                                using (var comandoPersona = new SqlCommand(consultaPersona, conexion, transaccion))
                                {
                                    comandoPersona.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entidad.NombreCompleto });
                                    comandoPersona.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entidad.Identificacion });
                                    comandoPersona.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entidad.Email ?? DBNull.Value });
                                    comandoPersona.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entidad.Telefono ?? DBNull.Value });
                                    comandoPersona.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entidad.Direccion ?? DBNull.Value });
                                    comandoPersona.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime) { Value = entidad.FechaRegistro });
                                    comandoPersona.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                                    personaId = Convert.ToInt32(comandoPersona.ExecuteScalar());
                                }
                            }

                            string consultaEmpleado = @"INSERT INTO dbo.Empleado (PersonaId, Puesto, Salario, FechaIngreso, Activo)
                                                        VALUES (@PersonaId, @Puesto, @Salario, @FechaIngreso, @Activo);
                                                        SELECT SCOPE_IDENTITY();";

                            int empleadoId;
                            using (var comandoEmpleado = new SqlCommand(consultaEmpleado, conexion, transaccion))
                            {
                                comandoEmpleado.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = personaId });
                                comandoEmpleado.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(entidad.Puesto) ? "Colaborador General" : entidad.Puesto });
                                comandoEmpleado.Parameters.Add(new SqlParameter("@Salario", SqlDbType.Decimal) { Value = entidad.Salario });
                                comandoEmpleado.Parameters.Add(new SqlParameter("@FechaIngreso", SqlDbType.DateTime) { Value = entidad.FechaIngreso });
                                comandoEmpleado.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                                empleadoId = Convert.ToInt32(comandoEmpleado.ExecuteScalar());
                            }

                            transaccion.Commit();
                            return empleadoId;
                        }
                        catch
                        {
                            transaccion.Rollback();
                            throw;
                        }
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public bool Actualizar(Empleado entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Empleado
                                        SET Puesto = @Puesto, Salario = @Salario, Activo = @Activo
                                        WHERE EmpleadoId = @EmpleadoId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = entidad.EmpleadoId });
                        comando.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(entidad.Puesto) ? "Colaborador General" : entidad.Puesto });
                        comando.Parameters.Add(new SqlParameter("@Salario", SqlDbType.Decimal) { Value = entidad.Salario });
                        comando.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                        conexion.Open();
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Empleado SET Activo = 0 WHERE EmpleadoId = @EmpleadoId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        private static Empleado MapearEmpleado(SqlDataReader lector)
        {
            return new Empleado
            {
                EmpleadoId = lector.GetInt32(lector.GetOrdinal("EmpleadoId")),
                PersonaId = lector.GetInt32(lector.GetOrdinal("PersonaId")),
                Puesto = lector.GetString(lector.GetOrdinal("Puesto")),
                Salario = lector.GetDecimal(lector.GetOrdinal("Salario")),
                FechaIngreso = lector.GetDateTime(lector.GetOrdinal("FechaIngreso")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo")),
                NombreCompleto = lector.GetString(lector.GetOrdinal("NombreCompleto")),
                Identificacion = lector.GetString(lector.GetOrdinal("Identificacion")),
                Email = lector.IsDBNull(lector.GetOrdinal("Email")) ? null : lector.GetString(lector.GetOrdinal("Email")),
                Telefono = lector.IsDBNull(lector.GetOrdinal("Telefono")) ? null : lector.GetString(lector.GetOrdinal("Telefono")),
                Direccion = lector.IsDBNull(lector.GetOrdinal("Direccion")) ? null : lector.GetString(lector.GetOrdinal("Direccion")),
                FechaRegistro = lector.GetDateTime(lector.GetOrdinal("FechaRegistro"))
            };
        }
    }
}
