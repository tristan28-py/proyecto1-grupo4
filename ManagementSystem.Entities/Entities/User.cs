using System;

namespace ManagementSystem.Entities.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Employee? Employee { get; set; }

        public virtual Role? Role { get; set; }
    }
}
