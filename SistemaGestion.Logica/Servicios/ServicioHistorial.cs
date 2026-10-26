using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestion.Entidades.Entidades;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Gestiona la pila de acciones recientes para consultar y deshacer los últimos cambios realizados.
    /// </summary>
    public class ServicioHistorial : IServicioHistorial
    {
        private readonly Stack<AccionHistorial> _pilaHistorial = new();
        private int _consecutivo = 1;

        /// <summary>
        /// Instancia compartida para mantener el historial de acciones durante toda la sesión de la aplicación.
        /// Inicia vacía para registrar las acciones reales del usuario.
        /// </summary>
        public static ServicioHistorial InstanciaCompartida { get; } = new ServicioHistorial();

        public int CantidadAcciones => _pilaHistorial.Count;

        /// <summary>
        /// Registra una nueva acción en la cima de la pila (operación Push).
        /// </summary>
        public AccionHistorial RegistrarAccion(
            string modulo,
            string descripcion,
            string tipoAccion = "Registro",
            int? entidadId = null,
            Action? accionDeshacer = null)
        {
            if (string.IsNullOrWhiteSpace(modulo))
            {
                throw new ArgumentException("El módulo de la acción es obligatorio.", nameof(modulo));
            }

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                throw new ArgumentException("La descripción de la acción es obligatoria.", nameof(descripcion));
            }

            var nuevaAccion = new AccionHistorial
            {
                AccionId = _consecutivo++,
                Modulo = modulo.Trim(),
                TipoAccion = string.IsNullOrWhiteSpace(tipoAccion) ? "Registro" : tipoAccion.Trim(),
                Descripcion = descripcion.Trim(),
                FechaHora = DateTime.Now,
                EntidadId = entidadId,
                AccionDeshacer = accionDeshacer
            };

            _pilaHistorial.Push(nuevaAccion);
            return nuevaAccion;
        }

        /// <summary>
        /// Registra un objeto AccionHistorial en la cima de la pila (operación Push).
        /// </summary>
        public AccionHistorial RegistrarAccion(AccionHistorial accion)
        {
            if (accion == null)
            {
                throw new ArgumentNullException(nameof(accion));
            }

            if (string.IsNullOrWhiteSpace(accion.Descripcion))
            {
                throw new ArgumentException("La descripción de la acción es obligatoria.", nameof(accion));
            }

            if (accion.AccionId <= 0)
            {
                accion.AccionId = _consecutivo++;
            }

            if (string.IsNullOrWhiteSpace(accion.Modulo))
            {
                accion.Modulo = "General";
            }

            _pilaHistorial.Push(accion);
            return accion;
        }

        /// <summary>
        /// Consulta la última acción registrada sin retirarla de la pila (operación Peek).
        /// </summary>
        public AccionHistorial? VerUltimaAccion()
        {
            return _pilaHistorial.Count > 0 ? _pilaHistorial.Peek() : null;
        }

        /// <summary>
        /// Retira y revierte la última acción registrada en orden LIFO (operación Pop).
        /// </summary>
        public AccionHistorial DeshacerUltimaAccion()
        {
            if (_pilaHistorial.Count == 0)
            {
                throw new InvalidOperationException("No existen acciones registradas en la pila para deshacer.");
            }

            AccionHistorial ultimaAccion = _pilaHistorial.Pop();
            ultimaAccion.AccionDeshacer?.Invoke();
            return ultimaAccion;
        }

        /// <summary>
        /// Obtiene la lista de acciones almacenadas en la pila (de la más reciente a la más antigua).
        /// </summary>
        public List<AccionHistorial> ObtenerHistorial()
        {
            return _pilaHistorial.ToList();
        }

        public void LimpiarHistorial()
        {
            _pilaHistorial.Clear();
        }
    }
}
