using System;
using System.Collections.Generic;

namespace SistemaGestion.Entidades.Entidades
{
    /// <summary>
    /// Entidad que representa un Rol de seguridad dentro del sistema.
    /// Ejemplos: Administrador, Vendedor, Bodeguero, Consulta.
    /// </summary>
    public class Rol : IEntidadActivable
    {
        private int _rolId;
        private string _nombreRol = string.Empty;
        private string? _descripcion;
        private bool _activo = true;

        public int RolId
        {
            get => _rolId;
            set => _rolId = value;
        }

        public string NombreRol
        {
            get => _nombreRol;
            set => _nombreRol = value?.Trim() ?? string.Empty;
        }

        public string? Descripcion
        {
            get => _descripcion;
            set => _descripcion = value?.Trim();
        }

        public bool Activo
        {
            get => _activo;
            set => _activo = value;
        }

        // Propiedad de navegación
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public int ObtenerIdentificador() => _rolId;

        public void Activar() => _activo = true;

        public void Desactivar() => _activo = false;

        public override string ToString() => _nombreRol;
    }
}
