using System;

namespace SistemaGestion.Entidades.Entidades
{
    /// <summary>
    /// Clase base con los datos personales comunes para clientes, empleados y usuarios.
    /// </summary>
    public class Persona : IEntidadActivable
    {
        private int _personaId;
        private string _nombreCompleto = string.Empty;
        private string _identificacion = string.Empty;
        private string? _email;
        private string? _telefono;
        private string? _direccion;
        private DateTime _fechaRegistro = DateTime.Now;
        private bool _activo = true;

        public int PersonaId
        {
            get => _personaId;
            set => _personaId = value;
        }

        public string NombreCompleto
        {
            get => _nombreCompleto;
            set => _nombreCompleto = value?.Trim() ?? string.Empty;
        }

        public string Identificacion
        {
            get => _identificacion;
            set => _identificacion = value?.Trim() ?? string.Empty;
        }

        public string? Email
        {
            get => _email;
            set => _email = value?.Trim();
        }

        public string? Telefono
        {
            get => _telefono;
            set => _telefono = value?.Trim();
        }

        public string? Direccion
        {
            get => _direccion;
            set => _direccion = value?.Trim();
        }

        public DateTime FechaRegistro
        {
            get => _fechaRegistro;
            set => _fechaRegistro = value;
        }

        public bool Activo
        {
            get => _activo;
            set => _activo = value;
        }

        // Propiedades de navegación
        public virtual Cliente? Cliente { get; set; }

        public virtual Empleado? Empleado { get; set; }

        /// <summary>
        /// Formatea el nombre y la identificación para uso en las clases hijas.
        /// </summary>
        protected string FormatearDatosBase()
        {
            return $"{_nombreCompleto} (ID: {_identificacion})";
        }

        /// <summary>
        /// Devuelve el tipo de registro según el perfil de la persona.
        /// </summary>
        public virtual string ObtenerTipoPersona() => "Persona";

        /// <summary>
        /// Devuelve un resumen legible con los datos principales del perfil.
        /// </summary>
        public virtual string ObtenerResumenPerfil()
        {
            return $"[{ObtenerTipoPersona()}] {FormatearDatosBase()}";
        }

        public int ObtenerIdentificador() => _personaId;

        public void Activar() => _activo = true;

        public void Desactivar() => _activo = false;

        public override string ToString() => ObtenerResumenPerfil();
    }
}
