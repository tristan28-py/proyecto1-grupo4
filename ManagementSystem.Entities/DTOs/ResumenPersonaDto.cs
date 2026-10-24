namespace ManagementSystem.Entities.DTOs
{
    public class ResumenPersonaDto
    {
        public int PersonaId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public bool Activo { get; set; }

        public bool EsCliente { get; set; }
        public int? ClienteId { get; set; }
        public decimal? LimiteCredito { get; set; }

        public bool EsEmpleado { get; set; }
        public int? EmpleadoId { get; set; }
        public string? Puesto { get; set; }
        public decimal? Salario { get; set; }

        public bool TieneUsuario { get; set; }
        public int? UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public int? RolId { get; set; }
        public string? NombreRol { get; set; }

        public string TiposPersona { get; set; } = string.Empty;
    }
}
