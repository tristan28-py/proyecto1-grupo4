using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ManagementSystem.Entities.Entities
{
    public class Categories
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;


    }
}
