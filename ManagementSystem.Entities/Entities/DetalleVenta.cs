using System;

namespace ManagementSystem.Entities.Entities
{
    public class DetalleVenta
    {
        public int DetalleVentaId { get; set; }

        public int VentaId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Descuento { get; set; }

        public decimal SubTotal { get; set; }

        // Propiedades de navegación
        public virtual Venta? Venta { get; set; }

        public virtual Producto? Producto { get; set; }
    }
}
