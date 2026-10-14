using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public int CurrentStock { get; set; }

        public int MinimumStock { get; set; }

        public int CategoryId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Category? Category { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

        public virtual ICollection<InventoryMovementHistory> InventoryMovements { get; set; } = new List<InventoryMovementHistory>();
    }
}
