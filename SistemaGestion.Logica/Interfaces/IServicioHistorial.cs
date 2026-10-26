using System;
using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de negocio para el historial de acciones recientes y deshacer con Stack&lt;AccionHistorial&gt; (LIFO).
    /// </summary>
    public interface IServicioHistorial
    {
        int CantidadAcciones { get; }
        AccionHistorial RegistrarAccion(string modulo, string descripcion, string tipoAccion = "Registro", int? entidadId = null, Action? accionDeshacer = null);
        AccionHistorial RegistrarAccion(AccionHistorial accion);
        AccionHistorial? VerUltimaAccion();
        AccionHistorial DeshacerUltimaAccion();
        List<AccionHistorial> ObtenerHistorial();
        void LimpiarHistorial();
    }
}
