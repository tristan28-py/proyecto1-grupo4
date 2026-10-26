using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Representa un evento o cambio registrado en el historial de actividades recientes.
    /// </summary>
    public class AccionHistorial
    {
        private int _accionId;
        private string _modulo = string.Empty;
        private string _tipoAccion = "Registro";
        private string _descripcion = string.Empty;
        private DateTime _fechaHora = DateTime.Now;
        private int? _entidadId;

        public int AccionId
        {
            get => _accionId;
            set => _accionId = value;
        }

        public string Modulo
        {
            get => _modulo;
            set => _modulo = value?.Trim() ?? string.Empty;
        }

        public string TipoAccion
        {
            get => _tipoAccion;
            set => _tipoAccion = value?.Trim() ?? string.Empty;
        }

        public string Descripcion
        {
            get => _descripcion;
            set => _descripcion = value?.Trim() ?? string.Empty;
        }

        public DateTime FechaHora
        {
            get => _fechaHora;
            set => _fechaHora = value;
        }

        public int? EntidadId
        {
            get => _entidadId;
            set => _entidadId = value;
        }

        /// <summary>
        /// Acción delegada opcional que permite revertir el cambio real en el sistema al ejecutar Pop().
        /// </summary>
        public Action? AccionDeshacer { get; set; }

        public override string ToString()
        {
            return $"[{_modulo}] {_descripcion}";
        }
    }
}
