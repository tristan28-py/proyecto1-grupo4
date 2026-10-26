using System.Collections.Generic;
using SistemaGestion.Entidades.DTOs;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.Interfaces
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
