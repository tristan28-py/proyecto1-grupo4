using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();
        Usuario? GetById(int id);
        Usuario? GetByUsername(string username);
        Usuario? GetByEmployeeId(int employeeId);
        int Add(Usuario entity);
        bool Update(Usuario entity);
        bool Delete(int id);
        Usuario? ValidateLogin(string username, string passwordHash);
        IEnumerable<Usuario> GetAllUsers();
    }

    /// <summary>
    /// Alias de compatibilidad con IUserRepository.
    /// </summary>
    public interface IUserRepository : IUsuarioRepository
    {
    }
}
