using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de negocio para la administración de pedidos pendientes con Queue&lt;Pedido&gt; (FIFO).
    /// </summary>
    public interface IServicioPedidos
    {
        int CantidadPendientes { get; }
        Pedido EncolarPedido(string nombreCliente, string nombreProducto, int cantidad, string? observaciones = null, int clienteId = 0, int productoId = 0);
        Pedido EncolarPedido(Pedido pedido);
        Pedido? VerSiguientePedido();
        Pedido AtenderSiguientePedido();
        List<Pedido> ObtenerPedidosPendientes();
        void LimpiarCola();
    }
}
