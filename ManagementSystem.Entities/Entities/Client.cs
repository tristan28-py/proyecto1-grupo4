using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Representa un Cliente en el sistema.
    /// Demuestra el principio de Herencia en POO heredando de la clase base Person.
    /// </summary>
    public class Client : Person
    {
        public int ClientId { get; set; }

        public decimal CreditLimit { get; set; }

        public DateTime AffiliationDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
