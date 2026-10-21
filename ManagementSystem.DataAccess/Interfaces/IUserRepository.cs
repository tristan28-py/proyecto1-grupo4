using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User? GetByUsername(string username);
        User? GetByEmployeeId(int employeeId);
        int Add(User entity);
        bool Update(User entity);
        bool Delete(int id);
        User? ValidateLogin(string username, string passwordHash);
        IEnumerable<User> GetAllUsers();
    }
}
