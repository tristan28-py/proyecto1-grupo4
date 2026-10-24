using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> GetAll();
        Cliente? GetById(int id);
        IEnumerable<Cliente> SearchClients(string term);
        int Add(Cliente entity);
        bool Update(Cliente entity);
        bool Delete(int id);
    }

    /// <summary>
    /// Alias de compatibilidad con IClientRepository.
    /// </summary>
    public interface IClientRepository : IClienteRepository
    {
    }
}
