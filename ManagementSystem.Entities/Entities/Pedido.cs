using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Entidad de dominio para la gestión de pedidos pendientes mediante la estructura Queue&lt;Pedido&gt; (FIFO).
    /// </summary>
    public class Pedido
    {
        private int _pedidoId;
        private int _clienteId;
        private string _nombreCliente = string.Empty;
        private int _productoId;
        private string _nombreProducto = string.Empty;
        private int _cantidad;
        private DateTime _horaRegistro = DateTime.Now;
        private string _observaciones = string.Empty;
        private bool _atendido;

        public int PedidoId
        {
            get => _pedidoId;
            set => _pedidoId = value;
        }

        public int ClienteId
        {
            get => _clienteId;
            set => _clienteId = value;
        }

        public string NombreCliente
        {
            get => _nombreCliente;
            set => _nombreCliente = value?.Trim() ?? string.Empty;
        }

        public int ProductoId
        {
            get => _productoId;
            set => _productoId = value;
        }

        public string NombreProducto
        {
            get => _nombreProducto;
            set => _nombreProducto = value?.Trim() ?? string.Empty;
        }

        public int Cantidad
        {
            get => _cantidad;
            set => _cantidad = value;
        }

        public DateTime HoraRegistro
        {
            get => _horaRegistro;
            set => _horaRegistro = value;
        }

        public string Observaciones
        {
            get => _observaciones;
            set => _observaciones = value?.Trim() ?? string.Empty;
        }

        public bool Atendido
        {
            get => _atendido;
            set => _atendido = value;
        }

        public override string ToString()
        {
            return $"Pedido #{_pedidoId} - {_nombreCliente} ({_nombreProducto} x{_cantidad})";
        }
    }
}
