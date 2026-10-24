using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IEmpleadoRepository
    {
        IEnumerable<Empleado> GetAll();
        Empleado? GetById(int id);
        Empleado? GetByPersonId(int personId);
        IEnumerable<Empleado> GetActiveEmployees();
        int Add(Empleado entity);
        bool Update(Empleado entity);
        bool Delete(int id);
    }

    /// <summary>
    /// Alias de compatibilidad con IEmployeeRepository.
    /// </summary>
    public interface IEmployeeRepository : IEmpleadoRepository
    {
    }
}
