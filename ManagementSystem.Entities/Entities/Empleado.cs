using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Empleado : Persona
    {
        public int EmpleadoId { get; set; }

        public string Puesto { get; set; } = string.Empty;

        public decimal Salario { get; set; }

        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public virtual ICollection<HistoricoMovimientosInventario> MovimientosInventario { get; set; } = new List<HistoricoMovimientosInventario>();
    }
}
