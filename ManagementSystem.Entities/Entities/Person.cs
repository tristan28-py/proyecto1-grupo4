using System;

namespace ManagementSystem.Entities.Entities
{
    public class Person
    {
        public int PersonId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Identification { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Client? Client { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
