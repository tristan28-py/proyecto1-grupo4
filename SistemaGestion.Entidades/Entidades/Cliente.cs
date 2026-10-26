using System;
using System.Collections.Generic;

namespace SistemaGestion.Entidades.Entidades
{
    /// <summary>
    /// Representa a un cliente registrado en el sistema junto con su límite de crédito.
    /// </summary>
    public class Cliente : Persona
    {
        private int _clienteId;
        private decimal _limiteCredito;
        private DateTime _fechaAfiliacion = DateTime.Now;

        public int ClienteId
        {
            get => _clienteId;
            set => _clienteId = value;
        }

        public decimal LimiteCredito
        {
            get => _limiteCredito;
            set => _limiteCredito = value;
        }

        public DateTime FechaAfiliacion
        {
            get => _fechaAfiliacion;
            set => _fechaAfiliacion = value;
        }

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();

        public override string ObtenerTipoPersona() => "Cliente";

        public override string ObtenerResumenPerfil()
        {
            return $"[{ObtenerTipoPersona()}] {FormatearDatosBase()} - Límite Crédito: {_limiteCredito:C2}";
        }
    }
}
