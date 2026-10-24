using System;

namespace ManagementSystem.Entities.Entities
{
    public class Usuario : Empleado
    {
        public int UsuarioId { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string ContrasenaHash { get; set; } = string.Empty;

        public int RolId { get; set; }

        public virtual Rol? Rol { get; set; }
    }
}
