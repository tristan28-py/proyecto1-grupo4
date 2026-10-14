using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int ClientId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        /// <summary>
        /// Suggested statuses: Pendiente, Procesada, Anulada (or Pending, Processed, Cancelled)
        /// </summary>
        public string Status { get; set; } = "Pendiente";

        public string? Observations { get; set; }

        // Navigation properties
        public virtual Client? Client { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
