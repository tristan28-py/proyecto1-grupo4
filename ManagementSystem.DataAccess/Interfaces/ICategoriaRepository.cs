using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetAll();
        IEnumerable<Categoria> GetActiveCategories();
        Categoria? GetById(int id);
        int Add(Categoria entity);
        bool Update(Categoria entity);
        bool Delete(int id);
    }

    /// <summary>
    /// Alias de compatibilidad con ICategoryRepository.
    /// </summary>
    public interface ICategoryRepository : ICategoriaRepository
    {
    }
}
