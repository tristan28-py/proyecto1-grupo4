using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un producto del inventario.
    /// Demuestra encapsulamiento mediante atributos privados y métodos públicos de validación y negocio.
    /// </summary>
    public class Producto : IEntidadActivable
    {
        private int _productoId;
        private string _nombre = string.Empty;
        private string? _descripcion;
        private decimal _precioCompra;
        private decimal _precioVenta;
        private int _stockActual;
        private int _stockMinimo;
        private int _categoriaId;
        private DateTime _fechaRegistro = DateTime.Now;
        private bool _activo = true;

        public int ProductoId
        {
            get => _productoId;
            set => _productoId = value;
        }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string? Descripcion
        {
            get => _descripcion;
            set => _descripcion = value?.Trim();
        }

        public decimal PrecioCompra
        {
            get => _precioCompra;
            set => _precioCompra = value;
        }

        public decimal PrecioVenta
        {
            get => _precioVenta;
            set => _precioVenta = value;
        }

        public int StockActual
        {
            get => _stockActual;
            set => _stockActual = value;
        }

        public int StockMinimo
        {
            get => _stockMinimo;
            set => _stockMinimo = value;
        }

        public int CategoriaId
        {
            get => _categoriaId;
            set => _categoriaId = value;
        }

        public DateTime FechaRegistro
        {
            get => _fechaRegistro;
            set => _fechaRegistro = value;
        }

        public bool Activo
        {
            get => _activo;
            set => _activo = value;
        }

        // Propiedades de navegación
        public virtual Categoria? Categoria { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

        public virtual ICollection<HistoricoMovimientosInventario> MovimientosInventario { get; set; } = new List<HistoricoMovimientosInventario>();

        /// <summary>
        /// Determina si el producto se encuentra en alerta de stock bajo (StockActual menor o igual a StockMinimo).
        /// </summary>
        public bool TieneStockBajo() => _stockActual <= _stockMinimo;

        /// <summary>
        /// Verifica si existe inventario suficiente para cubrir una cantidad solicitada en una venta.
        /// </summary>
        public bool TieneStockSuficiente(int cantidadSolicitada) => cantidadSolicitada > 0 && _stockActual >= cantidadSolicitada;

        /// <summary>
        /// Calcula el margen de ganancia unitario del producto.
        /// </summary>
        public decimal CalcularMargenGanancia() => _precioVenta - _precioCompra;

        public int ObtenerIdentificador() => _productoId;

        public void Activar() => _activo = true;

        public void Desactivar() => _activo = false;

        public override string ToString() => _nombre;
    }
}
