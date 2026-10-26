using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Interfaz común para las entidades que manejan estado activo o inactivo (borrado lógico).
    /// </summary>
    public interface IEntidadActivable
    {
        int ObtenerIdentificador();
        bool Activo { get; set; }
        void Activar();
        void Desactivar();
    }
}
