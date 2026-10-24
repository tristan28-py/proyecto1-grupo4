using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public int StockActual { get; set; }

        public int StockMinimo { get; set; }

        public int CategoriaId { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        // Propiedades de navegación
        public virtual Categoria? Categoria { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

        public virtual ICollection<HistoricoMovimientosInventario> MovimientosInventario { get; set; } = new List<HistoricoMovimientosInventario>();
    }
}
