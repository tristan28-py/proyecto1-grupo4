using System;

namespace ManagementSystem.Entities.Entities
{
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal SubTotal { get; set; }

        // Navigation properties
        public virtual Sale? Sale { get; set; }

        public virtual Product? Product { get; set; }
    }
}
