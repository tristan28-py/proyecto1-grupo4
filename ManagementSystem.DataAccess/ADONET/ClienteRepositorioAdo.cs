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
    /// Repositorio ADO.NET para la entidad Cliente (hereda de Persona).
    /// Utiliza transacciones al insertar/actualizar Persona + Cliente y cierra la conexión en finally.
    /// </summary>
    public class ClienteRepositorioAdo : IClienteRepositorio
    {
        public IEnumerable<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT c.ClienteId, c.PersonaId, c.LimiteCredito, c.FechaAfiliacion, c.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Cliente c
                                        INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                        ORDER BY p.NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearCliente(lector));
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

        public Cliente? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT c.ClienteId, c.PersonaId, c.LimiteCredito, c.FechaAfiliacion, c.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Cliente c
                                        INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                        WHERE c.ClienteId = @ClienteId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearCliente(lector);
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

        public IEnumerable<Cliente> BuscarClientes(string termino)
        {
            var lista = new List<Cliente>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT c.ClienteId, c.PersonaId, c.LimiteCredito, c.FechaAfiliacion, c.Activo,
                                               p.NombreCompleto, p.Identificacion, p.Email, p.Telefono, p.Direccion, p.FechaRegistro
                                        FROM dbo.Cliente c
                                        INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                        WHERE c.Activo = 1 AND (p.NombreCompleto LIKE @Termino OR p.Identificacion LIKE @Termino)
                                        ORDER BY p.NombreCompleto";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Termino", SqlDbType.VarChar, 100) { Value = $"%{termino}%" });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearCliente(lector));
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

        public int Insertar(Cliente entidad)
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

                            string consultaCliente = @"INSERT INTO dbo.Cliente (PersonaId, LimiteCredito, FechaAfiliacion, Activo)
                                                       VALUES (@PersonaId, @LimiteCredito, @FechaAfiliacion, @Activo);
                                                       SELECT SCOPE_IDENTITY();";

                            int clienteId;
                            using (var comandoCliente = new SqlCommand(consultaCliente, conexion, transaccion))
                            {
                                comandoCliente.Parameters.Add(new SqlParameter("@PersonaId", SqlDbType.Int) { Value = personaId });
                                comandoCliente.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal) { Value = entidad.LimiteCredito });
                                comandoCliente.Parameters.Add(new SqlParameter("@FechaAfiliacion", SqlDbType.DateTime) { Value = entidad.FechaAfiliacion });
                                comandoCliente.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                                clienteId = Convert.ToInt32(comandoCliente.ExecuteScalar());
                            }

                            transaccion.Commit();
                            return clienteId;
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

        public bool Actualizar(Cliente entidad)
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
                            string consultaPersona = @"UPDATE dbo.Persona 
                                                       SET NombreCompleto = @NombreCompleto, Identificacion = @Identificacion,
                                                           Email = @Email, Telefono = @Telefono, Direccion = @Direccion,
                                                           Activo = @Activo
                                                       WHERE PersonaId = (SELECT PersonaId FROM dbo.Cliente WHERE ClienteId = @ClienteId)";

                            using (var comandoPersona = new SqlCommand(consultaPersona, conexion, transaccion))
                            {
                                comandoPersona.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = entidad.ClienteId });
                                comandoPersona.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.VarChar, 200) { Value = entidad.NombreCompleto });
                                comandoPersona.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.VarChar, 50) { Value = entidad.Identificacion });
                                comandoPersona.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150) { Value = (object?)entidad.Email ?? DBNull.Value });
                                comandoPersona.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 50) { Value = (object?)entidad.Telefono ?? DBNull.Value });
                                comandoPersona.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 255) { Value = (object?)entidad.Direccion ?? DBNull.Value });
                                comandoPersona.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                                comandoPersona.ExecuteNonQuery();
                            }

                            string consultaCliente = @"UPDATE dbo.Cliente 
                                                       SET LimiteCredito = @LimiteCredito, Activo = @Activo
                                                       WHERE ClienteId = @ClienteId";

                            using (var comandoCliente = new SqlCommand(consultaCliente, conexion, transaccion))
                            {
                                comandoCliente.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = entidad.ClienteId });
                                comandoCliente.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal) { Value = entidad.LimiteCredito });
                                comandoCliente.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit) { Value = entidad.Activo });

                                comandoCliente.ExecuteNonQuery();
                            }

                            transaccion.Commit();
                            return true;
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

        public bool Eliminar(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Cliente SET Activo = 0 WHERE ClienteId = @ClienteId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = id });
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

        private static Cliente MapearCliente(SqlDataReader lector)
        {
            return new Cliente
            {
                ClienteId = lector.GetInt32(lector.GetOrdinal("ClienteId")),
                PersonaId = lector.GetInt32(lector.GetOrdinal("PersonaId")),
                NombreCompleto = lector.GetString(lector.GetOrdinal("NombreCompleto")),
                Identificacion = lector.GetString(lector.GetOrdinal("Identificacion")),
                Email = lector.IsDBNull(lector.GetOrdinal("Email")) ? null : lector.GetString(lector.GetOrdinal("Email")),
                Telefono = lector.IsDBNull(lector.GetOrdinal("Telefono")) ? null : lector.GetString(lector.GetOrdinal("Telefono")),
                Direccion = lector.IsDBNull(lector.GetOrdinal("Direccion")) ? null : lector.GetString(lector.GetOrdinal("Direccion")),
                FechaRegistro = lector.GetDateTime(lector.GetOrdinal("FechaRegistro")),
                LimiteCredito = lector.GetDecimal(lector.GetOrdinal("LimiteCredito")),
                FechaAfiliacion = lector.GetDateTime(lector.GetOrdinal("FechaAfiliacion")),
                Activo = lector.GetBoolean(lector.GetOrdinal("Activo"))
            };
        }
    }
}
