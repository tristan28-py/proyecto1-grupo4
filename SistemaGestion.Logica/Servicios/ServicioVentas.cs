using System;
using System.Collections.Generic;
using System.Linq;
using ManagementSystem.DataAccess.ADONET;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio de negocio para la gestión y validación de Ventas.
    /// Cumple las reglas obligatorias de la rúbrica:
    /// - No se puede vender una cantidad superior al stock disponible.
    /// - Una venta debe tener al menos un detalle.
    /// - El subtotal debe ser Cantidad * Precio.
    /// - El total de la venta debe corresponder a la suma de sus subtotales.
    /// - Al confirmar una venta, el stock debe disminuir y se debe registrar en el histórico el movimiento.
    /// - Una venta no confirmada no debe afectar el inventario.
    /// </summary>
    public class ServicioVentas : IServicioVentas
    {
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IServicioHistorial _servicioHistorial;

        public ServicioVentas()
            : this(new VentaRepositorioAdo(), new ProductoRepositorioAdo(), ServicioHistorial.InstanciaCompartida)
        {
        }

        public ServicioVentas(
            IVentaRepositorio ventaRepositorio,
            IProductoRepositorio productoRepositorio,
            IServicioHistorial? servicioHistorial = null)
        {
            _ventaRepositorio = ventaRepositorio ?? throw new ArgumentNullException(nameof(ventaRepositorio));
            _productoRepositorio = productoRepositorio ?? throw new ArgumentNullException(nameof(productoRepositorio));
            _servicioHistorial = servicioHistorial ?? ServicioHistorial.InstanciaCompartida;
        }

        /// <summary>
        /// Calcula y valida el subtotal de una línea de venta: (Cantidad * PrecioUnitario) - Descuento.
        /// </summary>
        public decimal CalcularSubTotalLinea(int cantidad, decimal precioUnitario, decimal descuento = 0m)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad a vender debe ser mayor a cero.", nameof(cantidad));
            }

            if (precioUnitario <= 0)
            {
                throw new ArgumentException("El precio unitario debe ser mayor a cero.", nameof(precioUnitario));
            }

            if (descuento < 0)
            {
                throw new ArgumentException("El descuento no puede ser negativo.", nameof(descuento));
            }

            decimal subTotal = (cantidad * precioUnitario) - descuento;
            if (subTotal < 0)
            {
                throw new ArgumentException("El descuento no puede superar el importe de la línea.", nameof(descuento));
            }

            return subTotal;
        }

        /// <summary>
        /// Calcula el total de una venta sumando los subtotales de cada detalle.
        /// Valida que la venta tenga al menos un detalle.
        /// </summary>
        public decimal CalcularTotalVenta(
            IEnumerable<DetalleVenta> detalles,
            decimal impuesto = 0m,
            decimal descuentoGeneral = 0m)
        {
            if (detalles == null)
            {
                throw new ArgumentNullException(nameof(detalles));
            }

            List<DetalleVenta> listaDetalles = detalles.ToList();
            if (listaDetalles.Count == 0)
            {
                throw new InvalidOperationException("Una venta debe tener al menos un detalle.");
            }

            if (impuesto < 0)
            {
                throw new ArgumentException("El impuesto no puede ser negativo.", nameof(impuesto));
            }

            if (descuentoGeneral < 0)
            {
                throw new ArgumentException("El descuento general no puede ser negativo.", nameof(descuentoGeneral));
            }

            decimal sumaSubTotales = 0m;
            foreach (DetalleVenta detalle in listaDetalles)
            {
                detalle.SubTotal = CalcularSubTotalLinea(detalle.Cantidad, detalle.PrecioUnitario, detalle.Descuento);
                sumaSubTotales += detalle.SubTotal;
            }

            decimal total = sumaSubTotales + impuesto - descuentoGeneral;
            if (total < 0)
            {
                throw new InvalidOperationException("El total de la venta no puede ser negativo.");
            }

            return total;
        }

        /// <summary>
        /// Valida que la cantidad solicitada de un producto no supere su StockActual disponible.
        /// </summary>
        public void ValidarStockDisponible(Producto producto, int cantidadSolicitada)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            if (cantidadSolicitada <= 0)
            {
                throw new ArgumentException("La cantidad solicitada debe ser mayor a cero.", nameof(cantidadSolicitada));
            }

            if (cantidadSolicitada > producto.StockActual)
            {
                throw new InvalidOperationException(
                    $"No se puede vender una cantidad superior al stock disponible para '{producto.Nombre}' (Disponible: {producto.StockActual}, Solicitado: {cantidadSolicitada}).");
            }
        }

        /// <summary>
        /// Valida todas las reglas de negocio de una venta y sus detalles antes de persistirla.
        /// </summary>
        public void ValidarVenta(Venta venta, IEnumerable<DetalleVenta> detalles)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            if (detalles == null)
            {
                throw new ArgumentNullException(nameof(detalles));
            }

            List<DetalleVenta> listaDetalles = detalles.ToList();
            if (listaDetalles.Count == 0)
            {
                throw new InvalidOperationException("Una venta debe tener al menos un detalle.");
            }

            if (venta.ClienteId <= 0)
            {
                throw new ArgumentException("Debe seleccionar un cliente válido para la venta.");
            }

            foreach (DetalleVenta detalle in listaDetalles)
            {
                detalle.SubTotal = CalcularSubTotalLinea(detalle.Cantidad, detalle.PrecioUnitario, detalle.Descuento);

                Producto? productoEnBd = _productoRepositorio.ObtenerPorId(detalle.ProductoId) ?? detalle.Producto;
                if (productoEnBd != null)
                {
                    ValidarStockDisponible(productoEnBd, detalle.Cantidad);
                }
            }

            venta.SubTotal = listaDetalles.Sum(d => d.SubTotal);
            venta.Total = CalcularTotalVenta(listaDetalles, venta.Impuesto, venta.Descuento);
        }

        /// <summary>
        /// Registra una venta utilizando List&lt;DetalleVenta&gt; y la transacción de base de datos en ADO.NET.
        /// Si confirmarVenta es true, la venta queda 'Procesada', disminuye el stock y registra en HistoricoMovimientosInventario.
        /// Si confirmarVenta es false, la venta queda 'Pendiente' y no altera el stock de inventario.
        /// </summary>
        public int RegistrarVenta(
            Venta venta,
            List<DetalleVenta> detalles,
            int empleadoId,
            bool confirmarVenta = true)
        {
            ValidarVenta(venta, detalles);

            int idEmpleadoEfectivo = empleadoId > 0 ? empleadoId : (venta.EmpleadoId > 0 ? venta.EmpleadoId : 1);
            venta.EmpleadoId = idEmpleadoEfectivo;
            venta.Estado = confirmarVenta ? Venta.EstadoProcesada : Venta.EstadoPendiente;
            venta.DetallesVenta = detalles;

            int ventaId = _ventaRepositorio.RegistrarVentaConTransaccion(venta, detalles, idEmpleadoEfectivo);
            venta.VentaId = ventaId;

            _servicioHistorial.RegistrarAccion(
                modulo: "Ventas",
                descripcion: $"Se registró la venta #{ventaId} ({venta.Estado}) por un total de {venta.Total:C2}",
                tipoAccion: "Venta",
                entidadId: ventaId);

            return ventaId;
        }

        public bool ConfirmarVentaPendiente(int ventaId, int empleadoId)
        {
            bool confirmada = _ventaRepositorio.ConfirmarVentaConTransaccion(ventaId, empleadoId > 0 ? empleadoId : 1);
            if (confirmada)
            {
                _servicioHistorial.RegistrarAccion(
                    modulo: "Ventas",
                    descripcion: $"Se confirmó la venta pendiente #{ventaId} y se actualizó el inventario",
                    tipoAccion: "Confirmación",
                    entidadId: ventaId);
            }
            return confirmada;
        }

        public List<Venta> ObtenerVentas()
        {
            return _ventaRepositorio.ObtenerTodos().ToList();
        }

        public List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId)
        {
            return _ventaRepositorio.ObtenerDetallesPorVenta(ventaId).ToList();
        }
    }
}
