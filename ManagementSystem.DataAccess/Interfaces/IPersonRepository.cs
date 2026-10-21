using System.Collections.Generic;
using ManagementSystem.Entities.Entities;
using ManagementSystem.Entities.DTOs;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person? GetById(int id);
        Person? GetByIdentification(string identification);
        IEnumerable<Person> Search(string term);
        int Add(Person entity);
        bool Update(Person entity);
        bool Delete(int id);
        IEnumerable<PersonSummaryDto> GetAllPersonSummaries();
        IEnumerable<PersonSummaryDto> SearchPersonSummaries(string? term, string? filterType);
    }
}
