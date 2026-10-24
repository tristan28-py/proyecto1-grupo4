using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class ReportesForm : Form
    {
        private class ProductoReporte
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Categoria { get; set; } = string.Empty;
            public int StockActual { get; set; }
            public int StockMinimo { get; set; }
            public decimal PrecioVenta { get; set; }
        }

        private class VentaReporte
        {
            public int VentaId { get; set; }
            public string Cliente { get; set; } = string.Empty;
            public decimal Monto { get; set; }
        }

        private readonly List<ProductoReporte> _productos = new();
        private readonly List<VentaReporte> _ventas = new();

        public ReportesForm()
        {
            InitializeComponent();
        }

        private void ReportesForm_Load(object? sender, EventArgs e)
        {
            CargarDatosDePrueba();
            cmbReportType.SelectedIndex = 0;
        }

        private void CargarDatosDePrueba()
        {
            _productos.Clear();
            _productos.AddRange(new[]
            {
                new ProductoReporte { Id = 1, Nombre = "Laptop Dell Inspiron 15", Categoria = "Electrónica y Cómputo", StockActual = 2, StockMinimo = 3, PrecioVenta = 750.00m },
                new ProductoReporte { Id = 2, Nombre = "Mouse Inalámbrico Logitech M185", Categoria = "Electrónica y Cómputo", StockActual = 40, StockMinimo = 5, PrecioVenta = 15.00m },
                new ProductoReporte { Id = 3, Nombre = "Teclado Mecánico RGB", Categoria = "Electrónica y Cómputo", StockActual = 3, StockMinimo = 4, PrecioVenta = 45.00m },
                new ProductoReporte { Id = 4, Nombre = "Monitor Samsung 24 pulg FHD", Categoria = "Electrónica y Cómputo", StockActual = 10, StockMinimo = 2, PrecioVenta = 140.00m },
                new ProductoReporte { Id = 5, Nombre = "Café Molido Premium 500g", Categoria = "Bebidas y Alimentos", StockActual = 8, StockMinimo = 10, PrecioVenta = 7.50m },
                new ProductoReporte { Id = 6, Nombre = "Resma Papel Bond A4 75g", Categoria = "Papelería y Oficina", StockActual = 100, StockMinimo = 20, PrecioVenta = 6.00m }
            });

            _ventas.Clear();
            _ventas.AddRange(new[]
            {
                new VentaReporte { VentaId = 1, Cliente = "María Fernández López", Monto = 765.00m },
                new VentaReporte { VentaId = 2, Cliente = "Comercial El Sol S.A.", Monto = 420.00m },
                new VentaReporte { VentaId = 3, Cliente = "María Fernández López", Monto = 140.00m },
                new VentaReporte { VentaId = 4, Cliente = "Jorge Castro Méndez", Monto = 90.00m },
                new VentaReporte { VentaId = 5, Cliente = "Comercial El Sol S.A.", Monto = 750.00m }
            });
        }

        private void ActualizarReporte()
        {
            dgvReports.Rows.Clear();
            string query = txtSearch.Text.Trim().ToLower();
            int tipo = cmbReportType.SelectedIndex;

            if (tipo == 0 || tipo == 1)
            {
                colCol1.HeaderText = "ID";
                colCol2.HeaderText = "Producto";
                colCol3.HeaderText = "Categoría";
                colCol4.HeaderText = "Stock / Mín.";
                colCol5.HeaderText = "Precio Venta";

                var consulta = _productos.AsEnumerable();

                if (tipo == 1)
                {
                    consulta = consulta.Where(p => p.StockActual <= p.StockMinimo);
                }

                if (!string.IsNullOrWhiteSpace(query))
                {
                    consulta = consulta.Where(p => p.Nombre.ToLower().Contains(query) ||
                                                   p.Categoria.ToLower().Contains(query));
                }

                var lista = consulta.OrderBy(p => p.PrecioVenta).ThenBy(p => p.Nombre).ToList();

                foreach (var p in lista)
                {
                    dgvReports.Rows.Add(
                        p.Id,
                        p.Nombre,
                        p.Categoria,
                        $"{p.StockActual} / {p.StockMinimo}",
                        p.PrecioVenta.ToString("C2")
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

                var agrupado = _ventas
                    .Where(v => string.IsNullOrWhiteSpace(query) || v.Cliente.ToLower().Contains(query))
                    .GroupBy(v => v.Cliente)
                    .Select((g, index) => new
                    {
                        Numero = index + 1,
                        Cliente = g.Key,
                        Cantidad = g.Count(),
                        Promedio = g.Average(x => x.Monto),
                        Total = g.Sum(x => x.Monto)
                    })
                    .OrderByDescending(x => x.Total)
                    .ToList();

                foreach (var item in agrupado)
                {
                    dgvReports.Rows.Add(
                        item.Numero,
                        item.Cliente,
                        item.Cantidad,
                        item.Promedio.ToString("C2"),
                        item.Total.ToString("C2")
                    );
                }

                ActualizarEstadisticas(agrupado.Select(a => a.Total).ToList());
            }
        }

        private void ActualizarEstadisticas(List<decimal> valores)
        {
            if (valores.Count == 0)
            {
                lblStatCount.Text = "Cantidad de registros: 0";
                lblStatSum.Text = "Sumatoria total: $0.00";
                lblStatAvg.Text = "Promedio: $0.00";
                lblStatMax.Text = "Valor máximo: $0.00";
                lblStatMin.Text = "Valor mínimo: $0.00";
                return;
            }

            lblStatCount.Text = $"Cantidad de registros: {valores.Count}";
            lblStatSum.Text = $"Sumatoria total: {valores.Sum():C2}";
            lblStatAvg.Text = $"Promedio: {valores.Average():C2}";
            lblStatMax.Text = $"Valor máximo: {valores.Max():C2}";
            lblStatMin.Text = $"Valor mínimo: {valores.Min():C2}";
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
