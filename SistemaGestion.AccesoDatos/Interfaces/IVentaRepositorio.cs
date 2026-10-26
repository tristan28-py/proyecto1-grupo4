using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Venta. Hereda de IRepositorio&lt;Venta&gt;.
    /// Incluye operaciones transaccionales para registro de venta, actualización de stock y auditoría de inventario.
    /// </summary>
    public interface IVentaRepositorio : IRepositorio<Venta>
    {
        int RegistrarVentaConTransaccion(Venta venta, IEnumerable<DetalleVenta> detalles, int empleadoId);
        bool ConfirmarVentaConTransaccion(int ventaId, int empleadoId);
        IEnumerable<DetalleVenta> ObtenerDetallesPorVenta(int ventaId);
        IEnumerable<HistoricoMovimientosInventario> ObtenerHistorialMovimientos();
    }
}
