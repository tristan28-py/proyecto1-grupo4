using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Empleado. Hereda de IRepositorio&lt;Empleado&gt;.
    /// </summary>
    public interface IEmpleadoRepositorio : IRepositorio<Empleado>
    {
        Empleado? ObtenerPorPersonaId(int personaId);
        IEnumerable<Empleado> ObtenerEmpleadosActivos();
    }
}
