using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SistemaGestion.AccesoDatos.Connection;
using SistemaGestion.AccesoDatos.Interfaces;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.ADONET
{
    /// <summary>
    /// Repositorio ADO.NET para la gestión de Ventas, Detalles de Venta e Histórico de Movimientos de Inventario.
    /// Implementa transacciones SQL (SqlTransaction), parámetros tipados, bloques using y cierre en finally.
    /// </summary>
    public class VentaRepositorioAdo : IVentaRepositorio
    {
        /// <summary>
        /// Registra el encabezado y los detalles de una venta dentro de una misma transacción.
        /// Cuando la venta está confirmada, rebaja las existencias y guarda el movimiento en el historial de inventario.
        /// </summary>
        public int RegistrarVentaConTransaccion(Venta venta, IEnumerable<DetalleVenta> detalles, int empleadoId)
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
                            // 1. Insertar Encabezado de Venta
                            string consultaVenta = @"INSERT INTO dbo.Venta 
                                                     (ClienteId, EmpleadoId, FechaVenta, SubTotal, Impuesto, Descuento, Total, Estado, Observaciones)
                                                     VALUES 
                                                     (@ClienteId, @EmpleadoId, @FechaVenta, @SubTotal, @Impuesto, @Descuento, @Total, @Estado, @Observaciones);
                                                     SELECT SCOPE_IDENTITY();";

                            int ventaId;
                            using (var comandoVenta = new SqlCommand(consultaVenta, conexion, transaccion))
                            {
                                comandoVenta.Parameters.Add(new SqlParameter("@ClienteId", SqlDbType.Int) { Value = venta.ClienteId });
                                comandoVenta.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = empleadoId });
                                comandoVenta.Parameters.Add(new SqlParameter("@FechaVenta", SqlDbType.DateTime) { Value = venta.FechaVenta });
                                comandoVenta.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = venta.SubTotal });
                                comandoVenta.Parameters.Add(new SqlParameter("@Impuesto", SqlDbType.Decimal) { Value = venta.Impuesto });
                                comandoVenta.Parameters.Add(new SqlParameter("@Descuento", SqlDbType.Decimal) { Value = venta.Descuento });
                                comandoVenta.Parameters.Add(new SqlParameter("@Total", SqlDbType.Decimal) { Value = venta.Total });
                                comandoVenta.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30) { Value = venta.Estado });
                                comandoVenta.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar, 500) { Value = (object?)venta.Observaciones ?? DBNull.Value });

                                ventaId = Convert.ToInt32(comandoVenta.ExecuteScalar());
                            }

                            bool ventaConfirmada = string.Equals(venta.Estado, Venta.EstadoProcesada, StringComparison.OrdinalIgnoreCase);

                            // 2. Insertar cada línea de detalle y, si la venta está confirmada, descontar stock y registrar en el histórico
                            foreach (var detalle in detalles)
                            {
                                string consultaDetalle = @"INSERT INTO dbo.DetalleVenta 
                                                           (VentaId, ProductoId, Cantidad, PrecioUnitario, Descuento, SubTotal)
                                                           VALUES 
                                                           (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario, @Descuento, @SubTotal);";

                                using (var comandoDetalle = new SqlCommand(consultaDetalle, conexion, transaccion))
                                {
                                    comandoDetalle.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = ventaId });
                                    comandoDetalle.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = detalle.ProductoId });
                                    comandoDetalle.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int) { Value = detalle.Cantidad });
                                    comandoDetalle.Parameters.Add(new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Value = detalle.PrecioUnitario });
                                    comandoDetalle.Parameters.Add(new SqlParameter("@Descuento", SqlDbType.Decimal) { Value = detalle.Descuento });
                                    comandoDetalle.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Decimal) { Value = detalle.SubTotal });

                                    comandoDetalle.ExecuteNonQuery();
                                }

                                if (ventaConfirmada)
                                {
                                    DescontarStockYRegistrarMovimiento(conexion, transaccion, detalle.ProductoId, detalle.Cantidad, empleadoId, ventaId);
                                }
                            }

                            transaccion.Commit();
                            return ventaId;
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

        /// <summary>
        /// Confirma una venta que estaba en estado 'Pendiente', disminuyendo el stock y registrando el movimiento en el histórico.
        /// </summary>
        public bool ConfirmarVentaConTransaccion(int ventaId, int empleadoId)
        {
            var detalles = ObtenerDetallesPorVenta(ventaId);
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    conexion.Open();
                    using (var transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            string consultaEstado = "SELECT Estado FROM dbo.Venta WHERE VentaId = @VentaId";
                            string? estadoActual;
                            using (var comandoEstado = new SqlCommand(consultaEstado, conexion, transaccion))
                            {
                                comandoEstado.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = ventaId });
                                estadoActual = comandoEstado.ExecuteScalar()?.ToString();
                            }

                            if (string.IsNullOrWhiteSpace(estadoActual))
                            {
                                throw new InvalidOperationException($"No se encontró la venta con ID {ventaId}.");
                            }

                            if (string.Equals(estadoActual, Venta.EstadoProcesada, StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }

                            foreach (var detalle in detalles)
                            {
                                DescontarStockYRegistrarMovimiento(conexion, transaccion, detalle.ProductoId, detalle.Cantidad, empleadoId, ventaId);
                            }

                            string consultaActualizar = "UPDATE dbo.Venta SET Estado = @Estado WHERE VentaId = @VentaId";
                            using (var comandoActualizar = new SqlCommand(consultaActualizar, conexion, transaccion))
                            {
                                comandoActualizar.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30) { Value = Venta.EstadoProcesada });
                                comandoActualizar.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = ventaId });
                                comandoActualizar.ExecuteNonQuery();
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

        private static void DescontarStockYRegistrarMovimiento(
            SqlConnection conexion,
            SqlTransaction transaccion,
            int productoId,
            int cantidadVendida,
            int empleadoId,
            int ventaId)
        {
            int stockAnterior;
            string consultaStock = "SELECT StockActual FROM dbo.Productos WHERE ProductoId = @ProductoId";
            using (var comandoStock = new SqlCommand(consultaStock, conexion, transaccion))
            {
                comandoStock.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = productoId });
                object resultadoStock = comandoStock.ExecuteScalar();
                stockAnterior = Convert.ToInt32(resultadoStock);
            }

            if (stockAnterior < cantidadVendida)
            {
                throw new InvalidOperationException($"No se puede vender una cantidad superior al stock disponible para el producto ID {productoId}. Disponible: {stockAnterior}, Solicitado: {cantidadVendida}.");
            }

            int stockNuevo = stockAnterior - cantidadVendida;

            string consultaActualizarStock = "UPDATE dbo.Productos SET StockActual = @StockNuevo WHERE ProductoId = @ProductoId";
            using (var comandoActualizarStock = new SqlCommand(consultaActualizarStock, conexion, transaccion))
            {
                comandoActualizarStock.Parameters.Add(new SqlParameter("@StockNuevo", SqlDbType.Int) { Value = stockNuevo });
                comandoActualizarStock.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = productoId });
                comandoActualizarStock.ExecuteNonQuery();
            }

            string consultaHistorial = @"INSERT INTO dbo.HistoricoMovimientosInventario 
                                         (ProductoId, EmpleadoId, FechaMovimiento, TipoMovimiento, Cantidad, StockAnterior, StockNuevo, Observacion)
                                         VALUES 
                                         (@ProductoId, @EmpleadoId, @FechaMovimiento, @TipoMovimiento, @Cantidad, @StockAnterior, @StockNuevo, @Observacion);";

            using (var comandoHistorial = new SqlCommand(consultaHistorial, conexion, transaccion))
            {
                comandoHistorial.Parameters.Add(new SqlParameter("@ProductoId", SqlDbType.Int) { Value = productoId });
                comandoHistorial.Parameters.Add(new SqlParameter("@EmpleadoId", SqlDbType.Int) { Value = empleadoId });
                comandoHistorial.Parameters.Add(new SqlParameter("@FechaMovimiento", SqlDbType.DateTime) { Value = DateTime.Now });
                comandoHistorial.Parameters.Add(new SqlParameter("@TipoMovimiento", SqlDbType.VarChar, 30) { Value = TipoMovimiento.Venta });
                comandoHistorial.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int) { Value = cantidadVendida });
                comandoHistorial.Parameters.Add(new SqlParameter("@StockAnterior", SqlDbType.Int) { Value = stockAnterior });
                comandoHistorial.Parameters.Add(new SqlParameter("@StockNuevo", SqlDbType.Int) { Value = stockNuevo });
                comandoHistorial.Parameters.Add(new SqlParameter("@Observacion", SqlDbType.VarChar, 255) { Value = $"Venta confirmada #{ventaId}" });

                comandoHistorial.ExecuteNonQuery();
            }
        }

        public int Insertar(Venta entidad)
        {
            return RegistrarVentaConTransaccion(entidad, entidad.DetallesVenta, entidad.EmpleadoId > 0 ? entidad.EmpleadoId : 1);
        }

        public bool Actualizar(Venta entidad)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"UPDATE dbo.Venta
                                        SET Estado = @Estado, Observaciones = @Observaciones
                                        WHERE VentaId = @VentaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = entidad.VentaId });
                        comando.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30) { Value = entidad.Estado });
                        comando.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar, 500) { Value = (object?)entidad.Observaciones ?? DBNull.Value });

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
            // Anulación lógica de la venta conservando el registro histórico
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = "UPDATE dbo.Venta SET Estado = @Estado WHERE VentaId = @VentaId";
                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30) { Value = Venta.EstadoAnulada });
                        comando.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = id });
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

        public Venta? ObtenerPorId(int id)
        {
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT v.VentaId, v.ClienteId, v.EmpleadoId, v.FechaVenta, v.SubTotal, v.Impuesto, v.Descuento, v.Total, v.Estado, v.Observaciones,
                                               p.NombreCompleto AS NombreCliente
                                        FROM dbo.Venta v
                                        INNER JOIN dbo.Cliente c ON v.ClienteId = c.ClienteId
                                        INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                        WHERE v.VentaId = @VentaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = id });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return MapearVenta(lector);
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

        public IEnumerable<Venta> ObtenerTodos()
        {
            var lista = new List<Venta>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT v.VentaId, v.ClienteId, v.EmpleadoId, v.FechaVenta, v.SubTotal, v.Impuesto, v.Descuento, v.Total, v.Estado, v.Observaciones,
                                               p.NombreCompleto AS NombreCliente
                                        FROM dbo.Venta v
                                        INNER JOIN dbo.Cliente c ON v.ClienteId = c.ClienteId
                                        INNER JOIN dbo.Persona p ON c.PersonaId = p.PersonaId
                                        ORDER BY v.FechaVenta DESC";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(MapearVenta(lector));
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

        public IEnumerable<DetalleVenta> ObtenerDetallesPorVenta(int ventaId)
        {
            var lista = new List<DetalleVenta>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT dv.DetalleVentaId, dv.VentaId, dv.ProductoId, dv.Cantidad, dv.PrecioUnitario, dv.Descuento, dv.SubTotal,
                                               p.Nombre AS NombreProducto
                                        FROM dbo.DetalleVenta dv
                                        INNER JOIN dbo.Productos p ON dv.ProductoId = p.ProductoId
                                        WHERE dv.VentaId = @VentaId";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("@VentaId", SqlDbType.Int) { Value = ventaId });
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                var detalle = new DetalleVenta
                                {
                                    DetalleVentaId = lector.GetInt32(lector.GetOrdinal("DetalleVentaId")),
                                    VentaId = lector.GetInt32(lector.GetOrdinal("VentaId")),
                                    ProductoId = lector.GetInt32(lector.GetOrdinal("ProductoId")),
                                    Cantidad = lector.GetInt32(lector.GetOrdinal("Cantidad")),
                                    PrecioUnitario = lector.GetDecimal(lector.GetOrdinal("PrecioUnitario")),
                                    Descuento = lector.GetDecimal(lector.GetOrdinal("Descuento")),
                                    SubTotal = lector.GetDecimal(lector.GetOrdinal("SubTotal")),
                                    Producto = new Producto
                                    {
                                        ProductoId = lector.GetInt32(lector.GetOrdinal("ProductoId")),
                                        Nombre = lector.GetString(lector.GetOrdinal("NombreProducto"))
                                    }
                                };
                                lista.Add(detalle);
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

        public IEnumerable<HistoricoMovimientosInventario> ObtenerHistorialMovimientos()
        {
            var lista = new List<HistoricoMovimientosInventario>();
            using (var conexion = ConfiguracionBaseDatos.CrearConexion())
            {
                try
                {
                    string consulta = @"SELECT h.MovimientoId, h.ProductoId, h.EmpleadoId, h.FechaMovimiento,
                                               h.TipoMovimiento, h.Cantidad, h.StockAnterior, h.StockNuevo, h.Observacion,
                                               p.Nombre AS NombreProducto
                                        FROM dbo.HistoricoMovimientosInventario h
                                        INNER JOIN dbo.Productos p ON h.ProductoId = p.ProductoId
                                        ORDER BY h.FechaMovimiento DESC";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (var lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                lista.Add(new HistoricoMovimientosInventario
                                {
                                    MovimientoId = lector.GetInt32(lector.GetOrdinal("MovimientoId")),
                                    ProductoId = lector.GetInt32(lector.GetOrdinal("ProductoId")),
                                    EmpleadoId = lector.GetInt32(lector.GetOrdinal("EmpleadoId")),
                                    FechaMovimiento = lector.GetDateTime(lector.GetOrdinal("FechaMovimiento")),
                                    TipoMovimiento = lector.GetString(lector.GetOrdinal("TipoMovimiento")),
                                    Cantidad = lector.GetInt32(lector.GetOrdinal("Cantidad")),
                                    StockAnterior = lector.GetInt32(lector.GetOrdinal("StockAnterior")),
                                    StockNuevo = lector.GetInt32(lector.GetOrdinal("StockNuevo")),
                                    Observacion = lector.IsDBNull(lector.GetOrdinal("Observacion")) ? null : lector.GetString(lector.GetOrdinal("Observacion")),
                                    Producto = new Producto
                                    {
                                        ProductoId = lector.GetInt32(lector.GetOrdinal("ProductoId")),
                                        Nombre = lector.GetString(lector.GetOrdinal("NombreProducto"))
                                    }
                                });
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

        private static Venta MapearVenta(SqlDataReader lector)
        {
            var venta = new Venta
            {
                VentaId = lector.GetInt32(lector.GetOrdinal("VentaId")),
                ClienteId = lector.GetInt32(lector.GetOrdinal("ClienteId")),
                EmpleadoId = lector.GetInt32(lector.GetOrdinal("EmpleadoId")),
                FechaVenta = lector.GetDateTime(lector.GetOrdinal("FechaVenta")),
                SubTotal = lector.GetDecimal(lector.GetOrdinal("SubTotal")),
                Impuesto = lector.GetDecimal(lector.GetOrdinal("Impuesto")),
                Descuento = lector.GetDecimal(lector.GetOrdinal("Descuento")),
                Total = lector.GetDecimal(lector.GetOrdinal("Total")),
                Estado = lector.GetString(lector.GetOrdinal("Estado")),
                Observaciones = lector.IsDBNull(lector.GetOrdinal("Observaciones")) ? null : lector.GetString(lector.GetOrdinal("Observaciones"))
            };

            if (!lector.IsDBNull(lector.GetOrdinal("NombreCliente")))
            {
                venta.Cliente = new Cliente
                {
                    ClienteId = venta.ClienteId,
                    NombreCompleto = lector.GetString(lector.GetOrdinal("NombreCliente"))
                };
            }

            return venta;
        }
    }
}
