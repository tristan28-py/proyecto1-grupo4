using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementSystem.Entities.Entities
{
    public class InvetoryMovementHistory
    {
        public int MovementId { get; set; }

        public int ProductId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;


    }
}
