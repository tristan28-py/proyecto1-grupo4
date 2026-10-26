using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Representa el encabezado de una factura o venta realizada a un cliente.
    /// </summary>
    public class Venta
    {
        public const string EstadoPendiente = "Pendiente";
        public const string EstadoProcesada = "Procesada";
        public const string EstadoAnulada = "Anulada";

        private int _ventaId;
        private int _clienteId;
        private int _empleadoId;
        private DateTime _fechaVenta = DateTime.Now;
        private decimal _subTotal;
        private decimal _impuesto;
        private decimal _descuento;
        private decimal _total;
        private string _estado = EstadoPendiente;
        private string? _observaciones;

        public int VentaId
        {
            get => _ventaId;
            set => _ventaId = value;
        }

        public int ClienteId
        {
            get => _clienteId;
            set => _clienteId = value;
        }

        public int EmpleadoId
        {
            get => _empleadoId;
            set => _empleadoId = value;
        }

        public DateTime FechaVenta
        {
            get => _fechaVenta;
            set => _fechaVenta = value;
        }

        public decimal SubTotal
        {
            get => _subTotal;
            set => _subTotal = value;
        }

        public decimal Impuesto
        {
            get => _impuesto;
            set => _impuesto = value;
        }

        public decimal Descuento
        {
            get => _descuento;
            set => _descuento = value;
        }

        public decimal Total
        {
            get => _total;
            set => _total = value;
        }

        /// <summary>
        /// Estados permitidos: Pendiente, Procesada, Anulada.
        /// </summary>
        public string Estado
        {
            get => _estado;
            set => _estado = string.IsNullOrWhiteSpace(value) ? EstadoPendiente : value.Trim();
        }

        public string? Observaciones
        {
            get => _observaciones;
            set => _observaciones = value?.Trim();
        }

        // Propiedades de navegación
        public virtual Cliente? Cliente { get; set; }

        public virtual Empleado? Empleado { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

        /// <summary>
        /// Actualiza el subtotal y el monto total sumando las líneas de detalle.
        /// </summary>
        public decimal RecalcularTotales()
        {
            _subTotal = DetallesVenta != null && DetallesVenta.Any()
                ? DetallesVenta.Sum(d => d.CalcularSubTotal())
                : 0m;

            _total = _subTotal + _impuesto - _descuento;
            if (_total < 0m)
            {
                _total = 0m;
            }

            return _total;
        }

        public bool EstaConfirmada() => string.Equals(_estado, EstadoProcesada, StringComparison.OrdinalIgnoreCase);
    }
}
