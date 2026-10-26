using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestion.AccesoDatos.EntityFramework;
using SistemaGestion.AccesoDatos.Interfaces;
using SistemaGestion.Entidades.DTOs;
using SistemaGestion.Entidades.Entidades;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio encargado de generar los reportes de inventario, ventas por cliente y estadísticas generales.
    /// </summary>
    public class ServicioReportes : IServicioReportes
    {
        private readonly IReporteRepositorio _reporteRepositorioEf;

        public ServicioReportes()
            : this(new ReporteRepositorioEf())
        {
        }

        public ServicioReportes(IReporteRepositorio reporteRepositorioEf)
        {
            _reporteRepositorioEf = reporteRepositorioEf ?? throw new ArgumentNullException(nameof(reporteRepositorioEf));
        }

        /// <summary>
        /// Filtra una colección List&lt;Producto&gt; para obtener únicamente los productos con stock bajo (StockActual &lt;= StockMinimo).
        /// </summary>
        public List<Producto> FiltrarProductosStockBajo(List<Producto> productos, string? textoBusqueda = null)
        {
            if (productos == null || productos.Count == 0)
            {
                return new List<Producto>();
            }

            IEnumerable<Producto> consulta = productos.Where(p => p.StockActual <= p.StockMinimo);

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                consulta = consulta.Where(p =>
                    p.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    (p.Categoria != null && p.Categoria.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase)));
            }

            return consulta
                .OrderBy(p => p.PrecioVenta)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        /// <summary>
        /// Ordena una colección List&lt;Producto&gt; por precio de venta ascendente y luego por nombre utilizando LINQ.
        /// </summary>
        public List<Producto> OrdenarProductosPorPrecioYNombre(List<Producto> productos, string? textoBusqueda = null)
        {
            if (productos == null || productos.Count == 0)
            {
                return new List<Producto>();
            }

            IEnumerable<Producto> consulta = productos;

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                consulta = consulta.Where(p =>
                    p.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    (p.Descripcion != null && p.Descripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Categoria != null && p.Categoria.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase)));
            }

            return consulta
                .OrderBy(p => p.PrecioVenta)
                .ThenBy(p => p.Nombre)
                .ToList();
        }

        /// <summary>
        /// Busca productos por coincidencia de texto en una colección List&lt;Producto&gt;.
        /// </summary>
        public List<Producto> BuscarProductosPorCoincidencia(List<Producto> productos, string textoBusqueda)
        {
            return OrdenarProductosPorPrecioYNombre(productos, textoBusqueda);
        }

        /// <summary>
        /// Agrupa una colección List&lt;Venta&gt; por cliente utilizando LINQ (Where, GroupBy, Select, Count, Average, Sum, Max, Min, OrderByDescending).
        /// </summary>
        public List<ReporteVentasClienteDto> ObtenerTotalVentasPorCliente(List<Venta> ventas, string? textoBusqueda = null)
        {
            if (ventas == null || ventas.Count == 0)
            {
                return new List<ReporteVentasClienteDto>();
            }

            IEnumerable<Venta> consulta = ventas.Where(v =>
                !string.Equals(v.Estado, Venta.EstadoAnulada, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                consulta = consulta.Where(v =>
                    (v.Cliente != null && v.Cliente.NombreCompleto.Contains(filtro, StringComparison.OrdinalIgnoreCase)) ||
                    (v.Observaciones != null && v.Observaciones.Contains(filtro, StringComparison.OrdinalIgnoreCase)));
            }

            var agrupado = consulta
                .GroupBy(v => new
                {
                    v.ClienteId,
                    NombreCliente = v.Cliente != null && !string.IsNullOrWhiteSpace(v.Cliente.NombreCompleto)
                        ? v.Cliente.NombreCompleto
                        : $"Cliente #{v.ClienteId}"
                })
                .Select(grupo => new ReporteVentasClienteDto
                {
                    ClienteId = grupo.Key.ClienteId,
                    NombreCliente = grupo.Key.NombreCliente,
                    CantidadCompras = grupo.Count(),
                    PromedioCompra = grupo.Average(x => x.Total),
                    TotalVentas = grupo.Sum(x => x.Total),
                    CompraMaxima = grupo.Max(x => x.Total),
                    CompraMinima = grupo.Min(x => x.Total)
                })
                .OrderByDescending(r => r.TotalVentas)
                .ToList();

            for (int i = 0; i < agrupado.Count; i++)
            {
                agrupado[i].NumeroFila = i + 1;
            }

            return agrupado;
        }

        /// <summary>
        /// Calcula cantidad, sumatoria, promedio, valor máximo y valor mínimo mediante operadores de agregación LINQ.
        /// </summary>
        public EstadisticasReporteDto CalcularEstadisticas(IEnumerable<decimal> valores)
        {
            List<decimal> listaValores = valores?.ToList() ?? new List<decimal>();
            if (listaValores.Count == 0)
            {
                return new EstadisticasReporteDto();
            }

            return new EstadisticasReporteDto
            {
                CantidadRegistros = listaValores.Count(),
                SumatoriaTotal = listaValores.Sum(),
                Promedio = listaValores.Average(),
                ValorMaximo = listaValores.Max(),
                ValorMinimo = listaValores.Min()
            };
        }

        /// <summary>
        /// Consulta productos con su categoría utilizando Entity Framework Core y aplica filtro opcional por texto.
        /// </summary>
        public List<ProductoCategoriaEfDto> ConsultarProductosConCategoriaEf(int? categoriaId = null, string? textoBusqueda = null)
        {
            List<ProductoCategoriaEfDto> datosEf = categoriaId.HasValue && categoriaId.Value > 0
                ? _reporteRepositorioEf.ObtenerProductosPorCategoriaEf(categoriaId.Value)
                : _reporteRepositorioEf.ObtenerProductosConCategoriaEf();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                datosEf = datosEf
                    .Where(p => p.NombreProducto.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                                p.NombreCategoria.Contains(filtro, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return datosEf;
        }

        /// <summary>
        /// Consulta productos con stock bajo directamente desde Entity Framework Core.
        /// </summary>
        public List<ProductoCategoriaEfDto> ConsultarProductosStockBajoEf(string? textoBusqueda = null)
        {
            List<ProductoCategoriaEfDto> datosEf = _reporteRepositorioEf.ObtenerProductosStockBajoEf();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                datosEf = datosEf
                    .Where(p => p.NombreProducto.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                                p.NombreCategoria.Contains(filtro, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return datosEf;
        }
    }
}
