using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        Employee? GetByPersonId(int personId);
        IEnumerable<Employee> GetActiveEmployees();
        int Add(Employee entity);
        bool Update(Employee entity);
        bool Delete(int id);
    }
}
