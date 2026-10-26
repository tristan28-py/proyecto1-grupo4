using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Rol. Hereda de IRepositorio&lt;Rol&gt;.
    /// </summary>
    public interface IRolRepositorio : IRepositorio<Rol>
    {
        IEnumerable<Rol> ObtenerRolesActivos();
    }
}
