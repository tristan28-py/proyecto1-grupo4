using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Entidad Usuario que hereda de Empleado (y por transitividad de Persona).
    /// Demuestra Herencia multinivel y Polimorfismo.
    /// </summary>
    public class Usuario : Empleado
    {
        private int _usuarioId;
        private string _nombreUsuario = string.Empty;
        private string _contrasenaHash = string.Empty;
        private int _rolId;

        public int UsuarioId
        {
            get => _usuarioId;
            set => _usuarioId = value;
        }

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set => _nombreUsuario = value?.Trim() ?? string.Empty;
        }

        public string ContrasenaHash
        {
            get => _contrasenaHash;
            set => _contrasenaHash = value ?? string.Empty;
        }

        public int RolId
        {
            get => _rolId;
            set => _rolId = value;
        }

        public virtual Rol? Rol { get; set; }

        public override string ObtenerTipoPersona() => "Usuario";

        public override string ObtenerResumenPerfil()
        {
            string nombreRol = Rol?.NombreRol ?? "Sin Rol";
            return $"[{ObtenerTipoPersona()}] {_nombreUsuario} ({nombreRol}) - {FormatearDatosBase()}";
        }
    }
}
