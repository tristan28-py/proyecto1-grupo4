using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Client
    {
        public int ClientId { get; set; }

        public int PersonId { get; set; }

        public decimal CreditLimit { get; set; }

        public DateTime AffiliationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Person? Person { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
