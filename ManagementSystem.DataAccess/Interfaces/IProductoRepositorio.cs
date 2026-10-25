using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Producto. Hereda de IRepositorio&lt;Producto&gt;.
    /// </summary>
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        IEnumerable<Producto> ObtenerActivos();
        IEnumerable<Producto> BuscarProductos(string terminoBusqueda);
        IEnumerable<Producto> ObtenerPorCategoria(int categoriaId);
        bool ActualizarStock(int productoId, int cambioCantidad);
    }
}
