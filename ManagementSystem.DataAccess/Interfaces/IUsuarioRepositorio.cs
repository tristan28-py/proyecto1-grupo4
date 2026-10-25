using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Usuario. Hereda de IRepositorio&lt;Usuario&gt;.
    /// </summary>
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario? ObtenerPorNombreUsuario(string nombreUsuario);
        Usuario? ObtenerPorEmpleadoId(int empleadoId);
        Usuario? ValidarCredenciales(string nombreUsuario, string contrasenaHash);
    }
}
