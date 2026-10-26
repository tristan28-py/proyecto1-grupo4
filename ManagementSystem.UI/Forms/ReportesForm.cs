using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;
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
                CargarDatosDePrueba();
            }

            if (_productos.Count == 0)
            {
                CargarDatosDePrueba();
            }
        }

        private void CargarDatosDePrueba()
        {
            _productos = new List<Producto>
            {
                new Producto { ProductoId = 1, Nombre = "Laptop Dell Inspiron 15", Categoria = new Categoria { Nombre = "Electrónica y Cómputo" }, StockActual = 2, StockMinimo = 3, PrecioVenta = 750.00m },
                new Producto { ProductoId = 2, Nombre = "Mouse Inalámbrico Logitech M185", Categoria = new Categoria { Nombre = "Electrónica y Cómputo" }, StockActual = 40, StockMinimo = 5, PrecioVenta = 15.00m },
                new Producto { ProductoId = 3, Nombre = "Teclado Mecánico RGB", Categoria = new Categoria { Nombre = "Electrónica y Cómputo" }, StockActual = 3, StockMinimo = 4, PrecioVenta = 45.00m },
                new Producto { ProductoId = 4, Nombre = "Monitor Samsung 24 pulg FHD", Categoria = new Categoria { Nombre = "Electrónica y Cómputo" }, StockActual = 10, StockMinimo = 2, PrecioVenta = 140.00m },
                new Producto { ProductoId = 5, Nombre = "Café Molido Premium 500g", Categoria = new Categoria { Nombre = "Bebidas y Alimentos" }, StockActual = 8, StockMinimo = 10, PrecioVenta = 7.50m },
                new Producto { ProductoId = 6, Nombre = "Resma Papel Bond A4 75g", Categoria = new Categoria { Nombre = "Papelería y Oficina" }, StockActual = 100, StockMinimo = 20, PrecioVenta = 6.00m }
            };

            _ventas = new List<Venta>
            {
                new Venta { VentaId = 1, ClienteId = 1, Cliente = new Cliente { ClienteId = 1, NombreCompleto = "Juan Pérez Gómez" }, Total = 765.00m, Estado = Venta.EstadoProcesada },
                new Venta { VentaId = 2, ClienteId = 2, Cliente = new Cliente { ClienteId = 2, NombreCompleto = "Empresa Alfa S.A." }, Total = 420.00m, Estado = Venta.EstadoProcesada },
                new Venta { VentaId = 3, ClienteId = 1, Cliente = new Cliente { ClienteId = 1, NombreCompleto = "Juan Pérez Gómez" }, Total = 140.00m, Estado = Venta.EstadoProcesada },
                new Venta { VentaId = 4, ClienteId = 2, Cliente = new Cliente { ClienteId = 2, NombreCompleto = "Empresa Alfa S.A." }, Total = 750.00m, Estado = Venta.EstadoProcesada }
            };
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
                colCol5.HeaderText = "Precio Venta";

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
                colCol4.HeaderText = "Promedio";
                colCol5.HeaderText = "Total Ventas";

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
