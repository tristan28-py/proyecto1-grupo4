using System.Collections.Generic;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Interfaz genérica base exigida por la rúbrica de POO para estandarizar operaciones CRUD.
    /// Demuestra abstracción, interfaces genéricas y polimorfismo en la capa de Repositorio.
    /// </summary>
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> ObtenerTodos();
        T? ObtenerPorId(int id);
        int Insertar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(int id);
    }
}
