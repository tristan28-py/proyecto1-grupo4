using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Clase base que representa una Persona dentro del sistema.
    /// Demuestra Herencia, Encapsulamiento (campos privados y método protegido) y Polimorfismo (métodos virtuales).
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
        /// Método protegido accesible únicamente por las clases derivadas (Cliente, Empleado, Usuario).
        /// </summary>
        protected string FormatearDatosBase()
        {
            return $"{_nombreCompleto} (ID: {_identificacion})";
        }

        /// <summary>
        /// Método virtual polimórfico que cada subclase sobrescribe para indicar su rol en el dominio.
        /// </summary>
        public virtual string ObtenerTipoPersona() => "Persona";

        /// <summary>
        /// Método virtual polimórfico para obtener el resumen descriptivo del perfil.
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
