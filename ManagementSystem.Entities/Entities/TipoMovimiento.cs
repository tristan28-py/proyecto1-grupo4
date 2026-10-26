using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Catálogo y constantes de tipos de movimiento de inventario.
    /// </summary>
    public class TipoMovimiento
    {
        public int TipoMovimientoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        // Tipos de movimiento utilizados en el historial de inventario
        public const string Entrada = "Entrada";
        public const string Salida = "Salida";
        public const string Ajuste = "Ajuste";
        public const string Devolucion = "Devolución";
        public const string Venta = "Venta";
    }
}
