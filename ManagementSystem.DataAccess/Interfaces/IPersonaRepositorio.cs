using System.Collections.Generic;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad base Persona. Hereda de IRepositorio&lt;Persona&gt;.
    /// </summary>
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Persona? ObtenerPorIdentificacion(string identificacion);
        IEnumerable<Persona> Buscar(string termino);
        IEnumerable<ResumenPersonaDto> ObtenerResumenPersonas();
        IEnumerable<ResumenPersonaDto> BuscarResumenPersonas(string? termino, string? filtroTipo);
    }
}
