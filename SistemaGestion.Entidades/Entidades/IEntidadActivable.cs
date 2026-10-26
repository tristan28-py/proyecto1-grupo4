using System;

namespace SistemaGestion.Entidades.Entidades
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
