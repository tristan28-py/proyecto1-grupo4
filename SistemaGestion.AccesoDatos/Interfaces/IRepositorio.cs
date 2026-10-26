using System.Collections.Generic;

namespace SistemaGestion.AccesoDatos.Interfaces
{
    /// <summary>
    /// Interfaz base genérica para las operaciones de lectura y escritura de datos.
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
