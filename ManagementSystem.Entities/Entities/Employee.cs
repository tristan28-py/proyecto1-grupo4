using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementSystem.Entities.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public int PersonId { get; set; }

        public string Position { get; set; }    

        public decimal Salary { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

    }
}
