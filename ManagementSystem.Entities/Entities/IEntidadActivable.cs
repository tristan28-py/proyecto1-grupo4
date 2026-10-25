using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Contrato de abstracción para las entidades del dominio que admiten borrado lógico (campo Activo).
    /// Demuestra el uso de abstracción, interfaces y polimorfismo en la capa de Dominio.
    /// </summary>
    public interface IEntidadActivable
    {
        int ObtenerIdentificador();
        bool Activo { get; set; }
        void Activar();
        void Desactivar();
    }
}
