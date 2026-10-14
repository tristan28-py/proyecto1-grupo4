using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Navigation properties (1:N relationship with Product)
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
