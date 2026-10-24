using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Cliente : Persona
    {
        public int ClienteId { get; set; }

        public decimal LimiteCredito { get; set; }

        public DateTime FechaAfiliacion { get; set; } = DateTime.Now;

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
