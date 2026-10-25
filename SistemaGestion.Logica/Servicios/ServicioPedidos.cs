using System;
using System.Collections.Generic;
using System.Linq;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio de negocio que administra los pedidos pendientes de atención mediante una Cola (Queue&lt;Pedido&gt; - FIFO).
    /// Cumple las operaciones mínimas exigidas en la rúbrica: Enqueue, Peek, Dequeue y Count.
    /// </summary>
    public class ServicioPedidos : IServicioPedidos
    {
        private readonly Queue<Pedido> _colaPedidos = new();
        private int _consecutivo = 101;

        /// <summary>
        /// Instancia compartida para conservar la cola de pedidos pendientes durante la sesión.
        /// </summary>
        public static ServicioPedidos InstanciaCompartida { get; } = CrearConDatosIniciales();

        public int CantidadPendientes => _colaPedidos.Count;

        private static ServicioPedidos CrearConDatosIniciales()
        {
            var servicio = new ServicioPedidos();
            servicio.EncolarPedido(new Pedido
            {
                NombreCliente = "Juan Pérez Gómez",
                NombreProducto = "Laptop Dell Inspiron 15",
                Cantidad = 2,
                HoraRegistro = DateTime.Now.AddMinutes(-20),
                Observaciones = "Entrega en sucursal central"
            });

            servicio.EncolarPedido(new Pedido
            {
                NombreCliente = "Empresa Alfa S.A.",
                NombreProducto = "Monitor Samsung 24 pulg FHD",
                Cantidad = 5,
                HoraRegistro = DateTime.Now.AddMinutes(-10),
                Observaciones = "Facturar a crédito corporativo"
            });

            return servicio;
        }

        /// <summary>
        /// Agrega un nuevo pedido al final de la cola de atención (operación Enqueue).
        /// </summary>
        public Pedido EncolarPedido(
            string nombreCliente,
            string nombreProducto,
            int cantidad,
            string? observaciones = null,
            int clienteId = 0,
            int productoId = 0)
        {
            var pedido = new Pedido
            {
                PedidoId = _consecutivo++,
                ClienteId = clienteId,
                NombreCliente = nombreCliente,
                ProductoId = productoId,
                NombreProducto = nombreProducto,
                Cantidad = cantidad,
                HoraRegistro = DateTime.Now,
                Observaciones = observaciones ?? string.Empty
            };

            return EncolarPedido(pedido);
        }

        /// <summary>
        /// Valida e inserta un objeto Pedido en la cola FIFO (operación Enqueue).
        /// </summary>
        public Pedido EncolarPedido(Pedido pedido)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido));
            }

            if (string.IsNullOrWhiteSpace(pedido.NombreCliente))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio para registrar un pedido.", nameof(pedido));
            }

            if (string.IsNullOrWhiteSpace(pedido.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio para registrar un pedido.", nameof(pedido));
            }

            if (pedido.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad del pedido debe ser mayor a cero.", nameof(pedido));
            }

            if (pedido.PedidoId <= 0)
            {
                pedido.PedidoId = _consecutivo++;
            }

            _colaPedidos.Enqueue(pedido);
            return pedido;
        }

        /// <summary>
        /// Consulta el siguiente pedido en turno sin retirarlo de la cola (operación Peek).
        /// </summary>
        public Pedido? VerSiguientePedido()
        {
            return _colaPedidos.Count > 0 ? _colaPedidos.Peek() : null;
        }

        /// <summary>
        /// Atiende y retira el primer pedido en la cola respetando el orden FIFO (operación Dequeue).
        /// </summary>
        public Pedido AtenderSiguientePedido()
        {
            if (_colaPedidos.Count == 0)
            {
                throw new InvalidOperationException("No hay pedidos pendientes en la cola para atender.");
            }

            Pedido pedidoAtendido = _colaPedidos.Dequeue();
            pedidoAtendido.Atendido = true;
            return pedidoAtendido;
        }

        /// <summary>
        /// Obtiene todos los pedidos pendientes en su orden actual de atención FIFO.
        /// </summary>
        public List<Pedido> ObtenerPedidosPendientes()
        {
            return _colaPedidos.ToList();
        }

        public void LimpiarCola()
        {
            _colaPedidos.Clear();
        }
    }
}
