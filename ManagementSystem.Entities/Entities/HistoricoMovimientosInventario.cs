using System;

namespace ManagementSystem.Entities.Entities
{
    public class HistoricoMovimientosInventario
    {
        public int MovimientoId { get; set; }

        public int ProductoId { get; set; }

        public int EmpleadoId { get; set; }

        public DateTime FechaMovimiento { get; set; } = DateTime.Now;

        /// <summary>
        /// Tipos de Movimiento: Entrada, Salida, Ajuste, Devolución, Venta
        /// </summary>
        public string TipoMovimiento { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public int StockAnterior { get; set; }

        public int StockNuevo { get; set; }

        public string? Observacion { get; set; }

        // Propiedades de navegación
        public virtual Producto? Producto { get; set; }

        public virtual Empleado? Empleado { get; set; }
    }
}
