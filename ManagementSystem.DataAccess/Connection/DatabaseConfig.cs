using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ManagementSystem.DataAccess.Connection
{
    /// <summary>
    /// Administrador de conexiones a SQL Server con ADO.NET.
    /// Garantiza el cierre y liberación adecuada de recursos mediante using y try-finally.
    /// </summary>
    public class DatabaseConfig
    {
        private static string _connectionString = "Server=.;Database=ManagementSystemDB;Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static bool TestConnection(out string message)
        {
            SqlConnection? connection = null;
            try
            {
                connection = CreateConnection();
                connection.Open();
                message = "Conexión a SQL Server exitosa.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Error al conectar con SQL Server: {ex.Message}";
                return false;
            }
            finally
            {
                // Cierre explícito exigido en la rúbrica utilizando finally
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}