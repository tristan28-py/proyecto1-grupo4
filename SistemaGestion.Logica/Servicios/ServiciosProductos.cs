using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestion.AccesoDatos.ADONET;
using SistemaGestion.AccesoDatos.Interfaces;
using SistemaGestion.Entidades.Entidades;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio encargado de validar y administrar el catálogo de productos.
    /// </summary>
    public class ServicioProductos : IServicioProductos
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IServicioHistorial _servicioHistorial;

        public ServicioProductos()
            : this(new ProductoRepositorioAdo(), ServicioHistorial.InstanciaCompartida)
        {
        }

        public ServicioProductos(IProductoRepositorio productoRepositorio, IServicioHistorial? servicioHistorial = null)
        {
            _productoRepositorio = productoRepositorio ?? throw new ArgumentNullException(nameof(productoRepositorio));
            _servicioHistorial = servicioHistorial ?? ServicioHistorial.InstanciaCompartida;
        }

        public List<Producto> ObtenerTodos()
        {
            List<Producto> listaProductos = _productoRepositorio.ObtenerTodos().ToList();
            return listaProductos.OrderBy(p => p.Nombre).ToList();
        }

        public List<Producto> ObtenerActivos()
        {
            List<Producto> listaProductos = _productoRepositorio.ObtenerActivos().ToList();
            return listaProductos.OrderBy(p => p.Nombre).ToList();
        }

        public Producto? ObtenerPorId(int productoId)
        {
            if (productoId <= 0)
            {
                return null;
            }
            return _productoRepositorio.ObtenerPorId(productoId);
        }

        /// <summary>
        /// Busca productos por coincidencia de texto utilizando LINQ sobre una colección List&lt;Producto&gt;.
        /// </summary>
        public List<Producto> BuscarProductos(string terminoBusqueda)
        {
            List<Producto> todos = ObtenerTodos();
            if (string.IsNullOrWhiteSpace(terminoBusqueda))
            {
                return todos;
            }

            string filtro = terminoBusqueda.Trim();
            return todos
                .Where(p => p.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                            (p.Descripcion != null && p.Descripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase)) ||
                            (p.Categoria != null && p.Categoria.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public List<Producto> ObtenerPorCategoria(int categoriaId)
        {
            return _productoRepositorio.ObtenerPorCategoria(categoriaId).ToList();
        }

        /// <summary>
        /// Filtra los productos con stock bajo (StockActual menor o igual a StockMinimo) mediante LINQ.
        /// </summary>
        public List<Producto> FiltrarProductosStockBajo()
        {
            List<Producto> productos = ObtenerTodos();
            return productos
                .Where(p => p.Activo && p.StockActual <= p.StockMinimo)
                .OrderBy(p => p.StockActual)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        /// <summary>
        /// Ordena los productos por precio de venta y luego por nombre utilizando LINQ (OrderBy / ThenBy).
        /// </summary>
        public List<Producto> OrdenarPorPrecioYNombre(IEnumerable<Producto>? coleccionBase = null)
        {
            IEnumerable<Producto> fuente = coleccionBase ?? ObtenerTodos();
            return fuente
                .OrderBy(p => p.PrecioVenta)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        /// <summary>
        /// Verifica que el producto tenga un nombre válido, precio de venta positivo y existencias no negativas.
        /// </summary>
        public bool ValidarProducto(Producto producto, out List<string> errores)
        {
            errores = new List<string>();

            if (producto == null)
            {
                errores.Add("Los datos del producto son obligatorios.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                errores.Add("El nombre del producto es obligatorio.");
            }
            else if (decimal.TryParse(producto.Nombre.Trim(), out _))
            {
                errores.Add("El nombre del producto no puede consistir únicamente en números.");
            }

            if (producto.PrecioVenta <= 0)
            {
                errores.Add("No se puede registrar un producto con precio menor o igual a cero.");
            }

            if (producto.PrecioCompra < 0)
            {
                errores.Add("El precio de compra no puede ser negativo.");
            }

            if (producto.StockActual < 0)
            {
                errores.Add("No se puede registrar una cantidad negativa de inventario.");
            }

            if (producto.StockMinimo < 0)
            {
                errores.Add("El stock mínimo no puede ser negativo.");
            }

            return errores.Count == 0;
        }

        public void ValidarProducto(Producto producto)
        {
            if (!ValidarProducto(producto, out List<string> errores))
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errores));
            }
        }

        public int AgregarProducto(Producto producto)
        {
            ValidarProducto(producto);
            int nuevoId = _productoRepositorio.Insertar(producto);
            producto.ProductoId = nuevoId;

            _servicioHistorial.RegistrarAccion(
                modulo: "Productos",
                descripcion: $"Se creó el producto '{producto.Nombre}' (ID #{nuevoId})",
                tipoAccion: "Creación",
                entidadId: nuevoId,
                accionDeshacer: () => _productoRepositorio.Eliminar(nuevoId));

            return nuevoId;
        }

        public bool ActualizarProducto(Producto producto)
        {
            ValidarProducto(producto);
            bool actualizado = _productoRepositorio.Actualizar(producto);

            if (actualizado)
            {
                _servicioHistorial.RegistrarAccion(
                    modulo: "Productos",
                    descripcion: $"Se actualizó el producto '{producto.Nombre}' (ID #{producto.ProductoId})",
                    tipoAccion: "Actualización",
                    entidadId: producto.ProductoId);
            }

            return actualizado;
        }

        public bool CambiarEstadoProducto(int productoId, bool nuevoEstado)
        {
            Producto? existente = _productoRepositorio.ObtenerPorId(productoId);
            if (existente == null)
            {
                return false;
            }

            bool estadoAnterior = existente.Activo;
            existente.Activo = nuevoEstado;
            bool resultado = _productoRepositorio.Actualizar(existente);

            if (resultado)
            {
                string estadoTexto = nuevoEstado ? "Activo" : "Inactivo";
                _servicioHistorial.RegistrarAccion(
                    modulo: "Productos",
                    descripcion: $"Se cambió el estado de '{existente.Nombre}' a {estadoTexto}",
                    tipoAccion: "Estado",
                    entidadId: productoId,
                    accionDeshacer: () =>
                    {
                        existente.Activo = estadoAnterior;
                        _productoRepositorio.Actualizar(existente);
                    });
            }

            return resultado;
        }

        public bool EliminarProducto(int productoId)
        {
            return CambiarEstadoProducto(productoId, false);
        }
    }

    /// <summary>
    /// Clase derivada de compatibilidad con el nombre en plural ServiciosProductos.
    /// </summary>
    public class ServiciosProductos : ServicioProductos
    {
        public ServiciosProductos() : base()
        {
        }

        public ServiciosProductos(IProductoRepositorio productoRepositorio) : base(productoRepositorio)
        {
        }
    }
}
