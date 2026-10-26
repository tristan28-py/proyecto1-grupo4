using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Operaciones disponibles para la administración y consulta de productos.
    /// </summary>
    public interface IServicioProductos
    {
        List<Producto> ObtenerTodos();
        List<Producto> ObtenerActivos();
        Producto? ObtenerPorId(int productoId);
        List<Producto> BuscarProductos(string terminoBusqueda);
        List<Producto> ObtenerPorCategoria(int categoriaId);
        List<Producto> FiltrarProductosStockBajo();
        List<Producto> OrdenarPorPrecioYNombre(IEnumerable<Producto>? coleccionBase = null);
        bool ValidarProducto(Producto producto, out List<string> errores);
        void ValidarProducto(Producto producto);
        int AgregarProducto(Producto producto);
        bool ActualizarProducto(Producto producto);
        bool CambiarEstadoProducto(int productoId, bool nuevoEstado);
        bool EliminarProducto(int productoId);
    }
}
