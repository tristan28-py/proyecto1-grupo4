using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SistemaGestion.AccesoDatos.Connection
{
    /// <summary>
    /// Administrador central de conexiones a SQL Server con ADO.NET.
    /// Garantiza el cierre y liberación adecuada de recursos mediante bloques using y try-finally.
    /// </summary>
    public class ConfiguracionBaseDatos
    {
        private static string _cadenaConexion = ResolverCadenaConexionInicial();

        public static string CadenaConexion
        {
            get => _cadenaConexion;
            set => _cadenaConexion = value;
        }

        /// <summary>
        /// Detecta automáticamente la instancia local disponible de SQL Server (.\SQLEXPRESS, . o LocalDB)
        /// donde se encuentre la base de datos SistemaGestionDB.
        /// </summary>
        private static string ResolverCadenaConexionInicial()
        {
            string[] servidoresCandidatos = new[]
            {
                @".\SQLEXPRESS",
                ".",
                @"(localdb)\MSSQLLocalDB"
            };

            foreach (string servidor in servidoresCandidatos)
            {
                string cadenaPrueba = $"Server={servidor};Database=SistemaGestionDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                SqlConnection? conexionPrueba = null;
                try
                {
                    using (conexionPrueba = new SqlConnection(cadenaPrueba))
                    {
                        conexionPrueba.Open();
                        return $"Server={servidor};Database=SistemaGestionDB;Integrated Security=True;TrustServerCertificate=True;";
                    }
                }
                catch
                {
                    // Continúa probando con la siguiente instancia local
                }
                finally
                {
                    if (conexionPrueba != null && conexionPrueba.State == ConnectionState.Open)
                    {
                        conexionPrueba.Close();
                    }
                }
            }

            return @"Server=.\SQLEXPRESS;Database=SistemaGestionDB;Integrated Security=True;TrustServerCertificate=True;";
        }

        /// <summary>
        /// Crea una nueva instancia de SqlConnection utilizando la cadena de conexión configurada.
        /// </summary>
        public static SqlConnection CrearConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }

        /// <summary>
        /// Verifica la conectividad con el servidor SQL Server cerrando explícitamente la conexión en el bloque finally.
        /// </summary>
        public static bool ProbarConexion(out string mensaje)
        {
            SqlConnection? conexion = null;
            try
            {
                using (conexion = CrearConexion())
                {
                    conexion.Open();
                    mensaje = "Conexión a SQL Server establecida exitosamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al conectar con SQL Server: {ex.Message}";
                return false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                    conexion.Dispose();
                }
            }
        }
    }
}
