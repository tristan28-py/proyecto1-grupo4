using System;

namespace ManagementSystem.Entities.Entities
{
    public class TipoMovimiento
    {
        public int TipoMovimientoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        // Constantes para los tipos de movimiento estándar
        public const string Entrada = "Entrada";
        public const string Salida = "Salida";
        public const string Ajuste = "Ajuste";
        public const string Devolucion = "Devolución";
        public const string Venta = "Venta";
    }
}
