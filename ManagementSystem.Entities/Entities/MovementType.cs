using System;

namespace ManagementSystem.Entities.Entities
{
    public class MovementType
    {
        public int MovementTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Constants for standard movement types
        public const string Entry = "Entrada";
        public const string Exit = "Salida";
        public const string Adjustment = "Ajuste";
        public const string Return = "Devolución";
        public const string Sale = "Venta";
    }
}
