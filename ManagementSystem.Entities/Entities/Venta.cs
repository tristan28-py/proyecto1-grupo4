using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Venta
    {
        public int VentaId { get; set; }

        public int ClienteId { get; set; }

        public int EmpleadoId { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.Now;

        public decimal SubTotal { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Descuento { get; set; }

        public decimal Total { get; set; }

        /// <summary>
        /// Estados sugeridos: Pendiente, Procesada, Anulada
        /// </summary>
        public string Estado { get; set; } = "Pendiente";

        public string? Observaciones { get; set; }

        // Propiedades de navegación
        public virtual Cliente? Cliente { get; set; }

        public virtual Empleado? Empleado { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
    }
}
