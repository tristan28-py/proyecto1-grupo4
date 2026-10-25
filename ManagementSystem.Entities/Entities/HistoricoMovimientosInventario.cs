using System;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Entidad que registra el historial de movimientos de inventario (Kardex / Auditoría).
    /// Se alimenta automáticamente al confirmar ventas o realizar ajustes de existencias.
    /// </summary>
    public class HistoricoMovimientosInventario
    {
        private int _movimientoId;
        private int _productoId;
        private int _empleadoId;
        private DateTime _fechaMovimiento = DateTime.Now;
        private string _tipoMovimiento = Entities.TipoMovimiento.Venta;
        private int _cantidad;
        private int _stockAnterior;
        private int _stockNuevo;
        private string? _observacion;

        public int MovimientoId
        {
            get => _movimientoId;
            set => _movimientoId = value;
        }

        public int ProductoId
        {
            get => _productoId;
            set => _productoId = value;
        }

        public int EmpleadoId
        {
            get => _empleadoId;
            set => _empleadoId = value;
        }

        public DateTime FechaMovimiento
        {
            get => _fechaMovimiento;
            set => _fechaMovimiento = value;
        }

        /// <summary>
        /// Tipos de Movimiento permitidos: Entrada, Salida, Ajuste, Devolución, Venta.
        /// </summary>
        public string TipoMovimiento
        {
            get => _tipoMovimiento;
            set => _tipoMovimiento = value?.Trim() ?? Entities.TipoMovimiento.Venta;
        }

        public int Cantidad
        {
            get => _cantidad;
            set => _cantidad = value;
        }

        public int StockAnterior
        {
            get => _stockAnterior;
            set => _stockAnterior = value;
        }

        public int StockNuevo
        {
            get => _stockNuevo;
            set => _stockNuevo = value;
        }

        public string? Observacion
        {
            get => _observacion;
            set => _observacion = value?.Trim();
        }

        // Propiedades de navegación
        public virtual Producto? Producto { get; set; }

        public virtual Empleado? Empleado { get; set; }
    }
}
