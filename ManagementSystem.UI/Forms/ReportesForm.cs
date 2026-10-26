using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaGestion.Entidades.DTOs;
using SistemaGestion.Entidades.Entidades;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class ReportesForm : Form
    {
        private readonly IServicioReportes _servicioReportes;
        private readonly IServicioProductos _servicioProductos;
        private readonly IServicioVentas _servicioVentas;

        private List<Producto> _productos = new();
        private List<Venta> _ventas = new();

        public ReportesForm()
        {
            InitializeComponent();
            _servicioReportes = new ServicioReportes();
            _servicioProductos = new ServicioProductos();
            _servicioVentas = new ServicioVentas();
        }

        private void ReportesForm_Load(object? sender, EventArgs e)
        {
            CargarDatosParaReportes();
            cmbReportType.SelectedIndex = 0;
        }

        private void CargarDatosParaReportes()
        {
            try
            {
                // Carga del catálogo de productos y ventas registradas para generar los reportes
                var productosEf = _servicioReportes.ConsultarProductosConCategoriaEf();
                if (productosEf.Count > 0)
                {
                    _productos = productosEf.Select(dto => new Producto
                    {
                        ProductoId = dto.ProductoId,
                        Nombre = dto.NombreProducto,
                        CategoriaId = dto.CategoriaId,
                        Categoria = new Categoria { CategoriaId = dto.CategoriaId, Nombre = dto.NombreCategoria },
                        PrecioCompra = dto.PrecioCompra,
                        PrecioVenta = dto.PrecioVenta,
                        StockActual = dto.StockActual,
                        StockMinimo = dto.StockMinimo,
                        Activo = dto.Activo
                    }).ToList();
                }
                else
                {
                    _productos = _servicioProductos.ObtenerTodos();
                }

                _ventas = _servicioVentas.ObtenerVentas();
            }
            catch
            {
                _productos.Clear();
                _ventas.Clear();
            }
        }

        private void CargarDatosDePrueba()
        {
            _productos.Clear();
            _ventas.Clear();
        }

        private void ActualizarReporte()
        {
            dgvReports.Rows.Clear();
            string textoBusqueda = txtSearch.Text.Trim();
            int tipo = cmbReportType.SelectedIndex;

            if (tipo == 0 || tipo == 1)
            {
                colCol1.HeaderText = "ID";
                colCol2.HeaderText = "Producto";
                colCol3.HeaderText = "Categoría";
                colCol4.HeaderText = "Stock / Mín.";
                colCol5.HeaderText = "Precio Venta (₡)";

                List<Producto> lista = tipo == 1
                    ? _servicioReportes.FiltrarProductosStockBajo(_productos, textoBusqueda)
                    : _servicioReportes.OrdenarProductosPorPrecioYNombre(_productos, textoBusqueda);

                foreach (var producto in lista)
                {
                    dgvReports.Rows.Add(
                        producto.ProductoId,
                        producto.Nombre,
                        producto.Categoria?.Nombre ?? "General",
                        $"{producto.StockActual} / {producto.StockMinimo}",
                        producto.PrecioVenta.ToString("C2")
                    );
                }

                ActualizarEstadisticas(lista.Select(p => p.PrecioVenta).ToList());
            }
            else if (tipo == 2)
            {
                colCol1.HeaderText = "#";
                colCol2.HeaderText = "Cliente";
                colCol3.HeaderText = "Compras";
                colCol4.HeaderText = "Promedio (₡)";
                colCol5.HeaderText = "Total Ventas (₡)";

                List<ReporteVentasClienteDto> agrupado = _servicioReportes.ObtenerTotalVentasPorCliente(_ventas, textoBusqueda);

                foreach (var item in agrupado)
                {
                    dgvReports.Rows.Add(
                        item.NumeroFila,
                        item.NombreCliente,
                        item.CantidadCompras,
                        item.PromedioCompra.ToString("C2"),
                        item.TotalVentas.ToString("C2")
                    );
                }

                ActualizarEstadisticas(agrupado.Select(a => a.TotalVentas).ToList());
            }
        }

        private void ActualizarEstadisticas(List<decimal> valores)
        {
            EstadisticasReporteDto estadisticas = _servicioReportes.CalcularEstadisticas(valores);

            lblStatCount.Text = $"Cantidad de registros: {estadisticas.CantidadRegistros}";
            lblStatSum.Text = $"Sumatoria total: {estadisticas.SumatoriaTotal:C2}";
            lblStatAvg.Text = $"Promedio: {estadisticas.Promedio:C2}";
            lblStatMax.Text = $"Valor máximo: {estadisticas.ValorMaximo:C2}";
            lblStatMin.Text = $"Valor mínimo: {estadisticas.ValorMinimo:C2}";
        }

        private void cmbReportType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarReporte();
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            ActualizarReporte();
        }
    }
}
