using System.Collections.Generic;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de negocio para la administración de Clientes, Empleados, Usuarios y Personas.
    /// </summary>
    public interface IServicioPersonas
    {
        List<Cliente> ObtenerClientes();
        List<Cliente> ObtenerClientesActivos();
        List<Empleado> ObtenerEmpleados();
        List<Rol> ObtenerRoles();
        List<ResumenPersonaDto> ObtenerResumenPersonas();
        List<ResumenPersonaDto> FiltrarResumenPersonas(string? textoBusqueda, string? tipoFiltro);
        List<string> ObtenerPerfilesPolimorficos();
        bool ValidarDatosPersona(string nombreCompleto, string identificacion, decimal montoAsignado, out List<string> errores);
        int GuardarPersonaSegunTipo(ResumenPersonaDto datos, string? contrasenaPlana = null);
        bool CambiarEstadoPersona(int personaId, bool nuevoEstado);
    }
}
