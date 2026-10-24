using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IProductoRepository
    {
        IEnumerable<Producto> GetAll();
        IEnumerable<Producto> GetActiveProducts();
        Producto? GetById(int id);
        IEnumerable<Producto> SearchProducts(string searchTerm);
        IEnumerable<Producto> GetProductsByCategory(int categoryId);
        int Add(Producto entity);
        bool Update(Producto entity);
        bool Delete(int id);
        bool UpdateStock(int productId, int quantityChange);
    }

    /// <summary>
    /// Alias de compatibilidad con IProductRepository.
    /// </summary>
    public interface IProductRepository : IProductoRepository
    {
    }
}
