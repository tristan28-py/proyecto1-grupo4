using System;

namespace SistemaGestion.Entidades.Entidades
{
    /// <summary>
    /// Representa una línea de producto incluida dentro de una venta.
    /// </summary>
    public class DetalleVenta
    {
        private int _detalleVentaId;
        private int _ventaId;
        private int _productoId;
        private int _cantidad;
        private decimal _precioUnitario;
        private decimal _descuento;
        private decimal _subTotal;

        public int DetalleVentaId
        {
            get => _detalleVentaId;
            set => _detalleVentaId = value;
        }

        public int VentaId
        {
            get => _ventaId;
            set => _ventaId = value;
        }

        public int ProductoId
        {
            get => _productoId;
            set => _productoId = value;
        }

        public int Cantidad
        {
            get => _cantidad;
            set => _cantidad = value;
        }

        public decimal PrecioUnitario
        {
            get => _precioUnitario;
            set => _precioUnitario = value;
        }

        public decimal Descuento
        {
            get => _descuento;
            set => _descuento = value;
        }

        public decimal SubTotal
        {
            get => _subTotal;
            set => _subTotal = value;
        }

        // Propiedades de navegación
        public virtual Venta? Venta { get; set; }

        public virtual Producto? Producto { get; set; }

        /// <summary>
        /// Calcula y actualiza el subtotal de la línea como (Cantidad * PrecioUnitario) - Descuento.
        /// </summary>
        public decimal CalcularSubTotal()
        {
            _subTotal = (_cantidad * _precioUnitario) - _descuento;
            return _subTotal;
        }
    }
}
