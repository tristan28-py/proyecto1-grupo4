using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Representa a un colaborador de la empresa con su puesto y salario asignado.
    /// </summary>
    public class Empleado : Persona
    {
        private int _empleadoId;
        private string _puesto = string.Empty;
        private decimal _salario;
        private DateTime _fechaIngreso = DateTime.Now;

        public int EmpleadoId
        {
            get => _empleadoId;
            set => _empleadoId = value;
        }

        public string Puesto
        {
            get => _puesto;
            set => _puesto = value?.Trim() ?? string.Empty;
        }

        public decimal Salario
        {
            get => _salario;
            set => _salario = value;
        }

        public DateTime FechaIngreso
        {
            get => _fechaIngreso;
            set => _fechaIngreso = value;
        }

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public virtual ICollection<HistoricoMovimientosInventario> MovimientosInventario { get; set; } = new List<HistoricoMovimientosInventario>();

        public override string ObtenerTipoPersona() => "Empleado";

        public override string ObtenerResumenPerfil()
        {
            return $"[{ObtenerTipoPersona()}] {FormatearDatosBase()} - Puesto: {_puesto}";
        }
    }
}
