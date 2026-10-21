namespace ManagementSystem.Entities.DTOs
{
    public class PersonSummaryDto
    {
        public int PersonId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }

        public bool IsClient { get; set; }
        public int? ClientId { get; set; }
        public decimal? CreditLimit { get; set; }

        public bool IsEmployee { get; set; }
        public int? EmployeeId { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }

        public bool HasUser { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }

        public string PersonTypes { get; set; } = string.Empty;
    }
}
