using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Rol
    {
        public int RolId { get; set; }

        public string NombreRol { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        // Propiedades de navegación
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
