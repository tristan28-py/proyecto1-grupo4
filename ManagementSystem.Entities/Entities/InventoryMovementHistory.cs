using System;

namespace ManagementSystem.Entities.Entities
{
    public class InventoryMovementHistory
    {
        public int MovementId { get; set; }

        public int ProductId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime MovementDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Movement Types: Entrada, Salida, Ajuste, Devolución, Venta (or Entry, Exit, Adjustment, Return, Sale)
        /// </summary>
        public string MovementType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int PreviousStock { get; set; }

        public int NewStock { get; set; }

        public string? Observation { get; set; }

        // Navigation properties
        public virtual Product? Product { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
