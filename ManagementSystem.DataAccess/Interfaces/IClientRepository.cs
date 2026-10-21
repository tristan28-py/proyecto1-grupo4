using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client? GetById(int id);
        IEnumerable<Client> SearchClients(string term);
        int Add(Client entity);
        bool Update(Client entity);
        bool Delete(int id);
    }
}
