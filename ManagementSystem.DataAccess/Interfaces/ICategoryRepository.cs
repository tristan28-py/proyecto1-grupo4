using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetActiveCategories();
        Category? GetById(int id);
        int Add(Category entity);
        bool Update(Category entity);
        bool Delete(int id);
    }
}
