using System;

namespace ManagementSystem.Entities.Entities
{
    public class Persona
    {
        public int PersonaId { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Identificacion { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        // Propiedades de navegación
        public virtual Cliente? Cliente { get; set; }

        public virtual Empleado? Empleado { get; set; }
    }
}
