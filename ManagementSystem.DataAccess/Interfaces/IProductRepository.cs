using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetActiveProducts();
        Product? GetById(int id);
        IEnumerable<Product> SearchProducts(string searchTerm);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        int Add(Product entity);
        bool Update(Product entity);
        bool Delete(int id);
        bool UpdateStock(int productId, int quantityChange);
    }
}
