using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Representa un Empleado en el sistema.
    /// Demuestra el principio de Herencia en POO heredando de la clase base Person.
    /// </summary>
    public class Employee : Person
    {
        public int EmployeeId { get; set; }

        public string Position { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public virtual ICollection<InventoryMovementHistory> InventoryMovements { get; set; } = new List<InventoryMovementHistory>();
    }
}
