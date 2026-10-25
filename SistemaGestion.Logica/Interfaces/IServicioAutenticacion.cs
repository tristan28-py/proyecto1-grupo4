using ManagementSystem.Entities.Entities;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de autenticación y seguridad de usuarios.
    /// </summary>
    public interface IServicioAutenticacion
    {
        string GenerarHashSha256(string textoPlano);
        Usuario? IniciarSesion(string nombreUsuario, string contrasenaPlana, out string mensajeError);
    }
}
