using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IRolRepository
    {
        IEnumerable<Rol> GetAllRoles();
        Rol? GetById(int roleId);
    }

    /// <summary>
    /// Alias de compatibilidad con IRoleRepository.
    /// </summary>
    public interface IRoleRepository : IRolRepository
    {
    }
}
