using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.EntityFramework
{
    /// <summary>
    /// Repositorio de consultas para el módulo de reportes implementado con Entity Framework Core.
    /// Se encarga de obtener los productos junto con su categoría, existencias bajas y movimientos de inventario,
    /// manteniendo las consultas analíticas separadas de las transacciones de escritura en ADO.NET.
    /// </summary>
    public class ReporteRepositorioEf : IReporteRepositorio
    {
        /// <summary>
        /// Consulta todos los productos activos junto con los datos de su categoría utilizando Entity Framework Core y LINQ.
        /// </summary>
        public List<ProductoCategoriaEfDto> ObtenerProductosConCategoriaEf()
        {
            using (var contexto = new SistemaGestionDbContext())
            {
                return contexto.Productos
                    .AsNoTracking()
                    .Include(p => p.Categoria)
                    .Where(p => p.Activo)
                    .OrderBy(p => p.PrecioVenta)
                    .ThenBy(p => p.Nombre)
                    .Select(p => new ProductoCategoriaEfDto
                    {
                        ProductoId = p.ProductoId,
                        NombreProducto = p.Nombre,
                        CategoriaId = p.CategoriaId,
                        NombreCategoria = p.Categoria != null ? p.Categoria.Nombre : "Sin Categoría",
                        PrecioCompra = p.PrecioCompra,
                        PrecioVenta = p.PrecioVenta,
                        StockActual = p.StockActual,
                        StockMinimo = p.StockMinimo,
                        StockBajo = p.StockActual <= p.StockMinimo,
                        ValorInventario = p.StockActual * p.PrecioVenta,
                        Activo = p.Activo
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Consulta los productos pertenecientes a una categoría específica utilizando Entity Framework Core.
        /// </summary>
        public List<ProductoCategoriaEfDto> ObtenerProductosPorCategoriaEf(int categoriaId)
        {
            using (var contexto = new SistemaGestionDbContext())
            {
                return contexto.Productos
                    .AsNoTracking()
                    .Include(p => p.Categoria)
                    .Where(p => p.Activo && p.CategoriaId == categoriaId)
                    .OrderBy(p => p.PrecioVenta)
                    .ThenBy(p => p.Nombre)
                    .Select(p => new ProductoCategoriaEfDto
                    {
                        ProductoId = p.ProductoId,
                        NombreProducto = p.Nombre,
                        CategoriaId = p.CategoriaId,
                        NombreCategoria = p.Categoria != null ? p.Categoria.Nombre : "Sin Categoría",
                        PrecioCompra = p.PrecioCompra,
                        PrecioVenta = p.PrecioVenta,
                        StockActual = p.StockActual,
                        StockMinimo = p.StockMinimo,
                        StockBajo = p.StockActual <= p.StockMinimo,
                        ValorInventario = p.StockActual * p.PrecioVenta,
                        Activo = p.Activo
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Obtiene mediante Entity Framework Core los productos cuyo StockActual es menor o igual a su StockMinimo.
        /// </summary>
        public List<ProductoCategoriaEfDto> ObtenerProductosStockBajoEf()
        {
            using (var contexto = new SistemaGestionDbContext())
            {
                return contexto.Productos
                    .AsNoTracking()
                    .Include(p => p.Categoria)
                    .Where(p => p.Activo && p.StockActual <= p.StockMinimo)
                    .OrderBy(p => p.StockActual)
                    .ThenBy(p => p.Nombre)
                    .Select(p => new ProductoCategoriaEfDto
                    {
                        ProductoId = p.ProductoId,
                        NombreProducto = p.Nombre,
                        CategoriaId = p.CategoriaId,
                        NombreCategoria = p.Categoria != null ? p.Categoria.Nombre : "Sin Categoría",
                        PrecioCompra = p.PrecioCompra,
                        PrecioVenta = p.PrecioVenta,
                        StockActual = p.StockActual,
                        StockMinimo = p.StockMinimo,
                        StockBajo = true,
                        ValorInventario = p.StockActual * p.PrecioVenta,
                        Activo = p.Activo
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Obtiene las ventas registradas con sus detalles mediante Entity Framework Core.
        /// </summary>
        public List<Venta> ObtenerVentasRegistradasEf()
        {
            using (var contexto = new SistemaGestionDbContext())
            {
                return contexto.Ventas
                    .AsNoTracking()
                    .Include(v => v.DetallesVenta)
                        .ThenInclude(d => d.Producto)
                    .OrderByDescending(v => v.FechaVenta)
                    .ToList();
            }
        }

        /// <summary>
        /// Obtiene el historial de movimientos de inventario (Kardex) con el producto asociado mediante Entity Framework Core.
        /// </summary>
        public List<HistoricoMovimientosInventario> ObtenerMovimientosInventarioEf()
        {
            using (var contexto = new SistemaGestionDbContext())
            {
                return contexto.HistoricoMovimientosInventario
                    .AsNoTracking()
                    .Include(h => h.Producto)
                    .OrderByDescending(h => h.FechaMovimiento)
                    .ToList();
            }
        }
    }
}
