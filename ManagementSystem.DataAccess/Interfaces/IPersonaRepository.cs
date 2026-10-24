using System.Collections.Generic;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetAll();
        Persona? GetById(int id);
        Persona? GetByIdentification(string identification);
        IEnumerable<Persona> Search(string term);
        int Add(Persona entity);
        bool Update(Persona entity);
        bool Delete(int id);
        IEnumerable<ResumenPersonaDto> GetAllPersonSummaries();
        IEnumerable<ResumenPersonaDto> SearchPersonSummaries(string? term, string? filterType);
    }

    /// <summary>
    /// Alias de compatibilidad con IPersonRepository.
    /// </summary>
    public interface IPersonRepository : IPersonaRepository
    {
    }
}
