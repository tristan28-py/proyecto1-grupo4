using System.Collections.Generic;
using SistemaGestion.Entidades.DTOs;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de reportes y consultas analíticas con LINQ sobre List&lt;T&gt; y Entity Framework Core.
    /// </summary>
    public interface IServicioReportes
    {
        List<Producto> FiltrarProductosStockBajo(List<Producto> productos, string? textoBusqueda = null);
        List<Producto> OrdenarProductosPorPrecioYNombre(List<Producto> productos, string? textoBusqueda = null);
        List<Producto> BuscarProductosPorCoincidencia(List<Producto> productos, string textoBusqueda);
        List<ReporteVentasClienteDto> ObtenerTotalVentasPorCliente(List<Venta> ventas, string? textoBusqueda = null);
        EstadisticasReporteDto CalcularEstadisticas(IEnumerable<decimal> valores);
        List<ProductoCategoriaEfDto> ConsultarProductosConCategoriaEf(int? categoriaId = null, string? textoBusqueda = null);
        List<ProductoCategoriaEfDto> ConsultarProductosStockBajoEf(string? textoBusqueda = null);
    }
}
