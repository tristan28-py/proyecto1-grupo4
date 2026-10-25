using ManagementSystem.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestion.Logica.Servicios
{
    public class ServiciosProductos
    {
        private readonly List<IProductRepository> _productoRepositories;

        public ServiciosProductos(List<IProductRepository   > productoRepositories)
        {
            _productoRepositories = productoRepositories;
        }

        public void GetallProductos(out List<ManagementSystem.Entities.Entities.Producto> productos)
        {
            productos = new List<ManagementSystem.Entities.Entities.Producto>();
            foreach (var repo in _productoRepositories)
            {
                productos.AddRange(repo.GetAll());
            }
        }

        // Validaciones

        public bool ValidarProducto(ManagementSystem.Entities.Entities.Producto producto, out List<string> errores)
        {
            errores = new List<string>();
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                errores.Add("El nombre del producto es obligatorio.");
            if (producto.PrecioVenta < 0)
                errores.Add("El precio de venta no puede ser negativo.");
            if (producto.Stock < 0)
            {
                errores.Add("El stock no puede ser negativo.");
            }

            if (producto.StockMinimo < 0)
                errores.Add("El stock mínimo no puede ser negativo.");
            return errores.Count == 0;
        }
        public void ValidarProducto(ManagementSystem.Entities.Entities.Producto producto)
        {
            if (!ValidarProducto(producto, out List<string> errores))
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errores));
            }
        } 
        public void AgregarProducto(ManagementSystem.Entities.Entities.Producto producto)
        {
            ValidarProducto(producto);
            foreach (var repo in _productoRepositories)
            {
                repo.Add(producto);
            }
        }
        public void ActualizarProducto(ManagementSystem.Entities.Entities.Producto producto)
        {
            ValidarProducto(producto);
            foreach (var repo in _productoRepositories)
            {
                repo.Update(producto);
            }
        }
        public void EliminarProducto(int id)
        {
            foreach (var repo in _productoRepositories)
            {
                repo.Delete(id);
            }
        }   
    }
}
