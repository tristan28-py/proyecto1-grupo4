using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de negocio para el registro de Ventas, cálculo de subtotales/totales y control de stock.
    /// </summary>
    public interface IServicioVentas
    {
        decimal CalcularSubTotalLinea(int cantidad, decimal precioUnitario, decimal descuento = 0m);
        decimal CalcularTotalVenta(IEnumerable<DetalleVenta> detalles, decimal impuesto = 0m, decimal descuentoGeneral = 0m);
        void ValidarStockDisponible(Producto producto, int cantidadSolicitada);
        void ValidarVenta(Venta venta, IEnumerable<DetalleVenta> detalles);
        int RegistrarVenta(Venta venta, List<DetalleVenta> detalles, int empleadoId, bool confirmarVenta = true);
        bool ConfirmarVentaPendiente(int ventaId, int empleadoId);
        List<Venta> ObtenerVentas();
        List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId);
    }
}
