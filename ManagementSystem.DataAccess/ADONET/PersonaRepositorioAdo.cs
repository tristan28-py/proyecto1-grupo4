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
    /// Repositorio ADO.NET para la entidad base Persona.
    /// Aplica parámetros SQL, bloques using, cierre en finally y mapeo mediante SqlDataReader.
    /// </summary>
    public class PersonaRepositorioAdo : IPersonaRepositorio
    {
        public IEnumerable<Persona> ObtenerTodos()
        {
            var lista = new List<Persona>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                        FROM dbo.Persona
                                        ORDER BY NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearPersona(lector));
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

        public Persona? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                        FROM dbo.Persona
                                        WHERE PersonaId = @PersonaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearPersona(lector);
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

        public Persona? ObtenerPorIdentificacion(string identificacion)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                        FROM dbo.Persona
                                        WHERE Identificacion = @Identificacion";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = identificacion.Trim() });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearPersona(lector);
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

        public IEnumerable<Persona> Buscar(string termino)
        {
            var lista = new List<Persona>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT PersonaId, NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo
                                        FROM dbo.Persona
                                        WHERE NombreCompleto LIKE @Termino OR Identificacion LIKE @Termino OR Email LIKE @Termino
                                        ORDER BY NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Termino", SqlDbType.VarChar, 100) { Value = $"%{termino}%" });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearPersona(lector));
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

        public int Insertar(Persona entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"INSERT INTO dbo.Persona (NombreCompleto, Identificacion, Email, Telefono, Direccion, FechaRegistro, Activo)
                                        VALUES (@NombreCompleto, @Identificacion, @Email, @Telefono, @Direccion, @FechaRegistro, @Activo);
                                        SELECT SCOPE_IDENTITY();";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entidad.NombreCompleto });
                        comando.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entidad.Identificacion });
                        comando.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entidad.Email ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entidad.Telefono ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entidad.Direccion ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@FechaRegistro", SqlDbType.DateTime) { Value = entidad.FechaRegistro });
                        comando.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                        conexion.Open();
                        return Convert.ToInt32(comando.ExecuteScalar());
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

        public bool Actualizar(Persona entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Persona
                                        SET NombreCompleto = @NombreCompleto, Identificacion = @Identificacion,
                                            Email = @Email, Telefono = @Telefono, Direccion = @Direccion,
                                            Activo = @Activo
                                        WHERE PersonaId = @PersonaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = entidad.PersonaId });
                        comando.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entidad.NombreCompleto });
                        comando.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entidad.Identificacion });
                        comando.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entidad.Email ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entidad.Telefono ?? DBNull.Value });
                        comando.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entidad.Direccion ?? DBNull.Value });
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
                    string consulta = "UPDATE dbo.Persona SET Activo = 0 WHERE PersonaId = @PersonaId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = id });
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

        public IEnumerable<ResumenPersonaDto> ObtenerResumenPersonas()
        {
            return BuscarResumenPersonas(null, null);
        }

        public IEnumerable<ResumenPersonaDto> BuscarResumenPersonas(string? termino, string? filtroTipo)
        {
            var lista = new List<ResumenPersonaDto>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"
                        SELECT p.PersonaId, p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.Activo,
                               c.ClienteId, c.LimiteCredito,
                               e.EmpleadoId, e.Puesto, e.Salario,
                               u.UsuarioId, u.NombreUsuario, u.RolId, r.NombreRol
                        FROM dbo.Persona p
                        LEFT JOIN dbo.Cliente c ON p.PersonaId = c.PersonaId
                        LEFT JOIN dbo.Empleado e ON p.PersonaId = e.PersonaId
                        LEFT JOIN dbo.Usuarios u ON e.EmpleadoId = u.EmpleadoId
                        LEFT JOIN dbo.Roles r ON u.RolId = r.RolId
                        WHERE 1 = 1";

                    if (!string.IsNullOrWhiteSpace(termino))
                    {
                        consulta += " AND (p.NombreCompleto LIKE @Termino OR p.Identificacion LIKE @Termino OR p.Email LIKE @Termino)";
                    }

                    if (!string.IsNullOrWhiteSpace(filtroTipo) && !filtroTipo.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                    {
                        if (filtroTipo.StartsWith("Cliente", StringComparison.OrdinalIgnoreCase))
                        {
                            consulta += " AND c.ClienteId IS NOT NULL";
                        }
                        else if (filtroTipo.StartsWith("Empleado", StringComparison.OrdinalIgnoreCase))
                        {
                            consulta += " AND e.EmpleadoId IS NOT NULL";
                        }
                        else if (filtroTipo.StartsWith("Usuario", StringComparison.OrdinalIgnoreCase))
                        {
                            consulta += " AND u.UsuarioId IS NOT NULL";
                        }
                    }

                    consulta += " ORDER BY p.NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        if (!string.IsNullOrWhiteSpace(termino))
                        {
                            comando.Parameters.Add(new SqlParameter("@Termino", SqlDbType.VarChar, 100) { Value = $"%{termino.Trim()}%" });
                        }

                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                var dto = new ResumenPersonaDto
                                {
                                    PersonaId = lector.GetInt32(lector.GetOrdinal("PersonaId")),
                                    NombreCompleto = lector.GetString(lector.GetOrdinal("NombreCompleto")),
                                    Identificacion = lector.GetString(lector.GetOrdinal("Identificacion")),
                                    Email = lector.IsDBNull(lector.GetOrdinal("Email")) ? null : lector.GetString(lector.GetOrdinal("Email")),
                                    Telefono = lector.IsDBNull(lector.GetOrdinal("Telefono")) ? null : lector.GetString(lector.GetOrdinal("Telefono")),
                                    Direccion = lector.IsDBNull(lector.GetOrdinal("Direccion")) ? null : lector.GetString(lector.GetOrdinal("Direccion")),
                                    Activo = lector.GetBoolean(lector.GetOrdinal("Activo")),

                                    EsCliente = !lector.IsDBNull(lector.GetOrdinal("ClienteId")),
                                    ClienteId = lector.IsDBNull(lector.GetOrdinal("ClienteId")) ? null : lector.GetInt32(lector.GetOrdinal("ClienteId")),
                                    LimiteCredito = lector.IsDBNull(lector.GetOrdinal("LimiteCredito")) ? null : lector.GetDecimal(lector.GetOrdinal("LimiteCredito")),

                                    EsEmpleado = !lector.IsDBNull(lector.GetOrdinal("EmpleadoId")),
                                    EmpleadoId = lector.IsDBNull(lector.GetOrdinal("EmpleadoId")) ? null : lector.GetInt32(lector.GetOrdinal("EmpleadoId")),
                                    Puesto = lector.IsDBNull(lector.GetOrdinal("Puesto")) ? null : lector.GetString(lector.GetOrdinal("Puesto")),
                                    Salario = lector.IsDBNull(lector.GetOrdinal("Salario")) ? null : lector.GetDecimal(lector.GetOrdinal("Salario")),

                                    TieneUsuario = !lector.IsDBNull(lector.GetOrdinal("UsuarioId")),
                                    UsuarioId = lector.IsDBNull(lector.GetOrdinal("UsuarioId")) ? null : lector.GetInt32(lector.GetOrdinal("UsuarioId")),
                                    NombreUsuario = lector.IsDBNull(lector.GetOrdinal("NombreUsuario")) ? null : lector.GetString(lector.GetOrdinal("NombreUsuario")),
                                    RolId = lector.IsDBNull(lector.GetOrdinal("RolId")) ? null : lector.GetInt32(lector.GetOrdinal("RolId")),
                                    NombreRol = lector.IsDBNull(lector.GetOrdinal("NombreRol")) ? null : lector.GetString(lector.GetOrdinal("NombreRol"))
                                };

                                if (dto.TieneUsuario)
                                    dto.TiposPersona = "Usuario";
                                else if (dto.EsEmpleado)
                                    dto.TiposPersona = "Empleado";
                                else if (dto.EsCliente)
                                    dto.TiposPersona = "Cliente";
                                else
                                    dto.TiposPersona = "Persona";

                                lista.Add(dto);
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

        private static Persona MapearPersona(SqlDataReader lector)
        {
            return new Persona
            {
                PersonaId = lector.GetInt32(lector.GetOrdinal("PersonaId")),
                NombreCompleto = lector.GetString(lector.GetOrdinal("NombreCompleto")),
                Identificacion = lector.GetString(lector.GetOrdinal("Identificacion")),
                Email = lector.IsDBNull(lector.GetOrdinal("Email")) ? null : lector.GetString(lector.GetOrdinal("Email")),
                Telefono = lector.IsDBNull(lector.GetOrdinal("Telefono")) ? null : lector.GetString(lector.GetOrdinal("Telefono")),
                Direccion = lector.IsDBNull(lector.GetOrdinal("Direccion")) ? null : lector.GetString(lector.GetOrdinal("Direccion")),
                FechaRegistro = lector.GetDateTime(lector.GetOrdinal("FechaRegistro")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo"))
            };
        }
    }
}
