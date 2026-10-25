using System.Collections.Generic;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Contrato de repositorio especializado en consultas y reportes mediante Entity Framework Core.
    /// Separa las consultas analíticas y proyecciones relacionales del acceso transaccional ADO.NET.
    /// </summary>
    public interface IReporteRepositorio
    {
        List<ProductoCategoriaEfDto> ObtenerProductosConCategoriaEf();
        List<ProductoCategoriaEfDto> ObtenerProductosPorCategoriaEf(int categoriaId);
        List<ProductoCategoriaEfDto> ObtenerProductosStockBajoEf();
        List<Venta> ObtenerVentasRegistradasEf();
        List<HistoricoMovimientosInventario> ObtenerMovimientosInventarioEf();
    }
}
