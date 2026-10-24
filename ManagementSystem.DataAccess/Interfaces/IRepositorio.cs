using System.Collections.Generic;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Interfaz genérica base exigida por la rúbrica de POO (IRepositorio<T> / IRepository<T>).
    /// </summary>
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        int Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }

    /// <summary>
    /// Alias de compatibilidad con IRepository.
    /// </summary>
    public interface IRepository<T> : IRepositorio<T> where T : class
    {
    }
}
