using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
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
