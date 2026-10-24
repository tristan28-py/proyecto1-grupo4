using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class VentasForm : Form
    {
        private class ProductoItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public decimal PrecioVenta { get; set; }
            public int StockDisponible { get; set; }

            public override string ToString() => Nombre;
        }

        private class DetalleVentaViewModel
        {
            public int ProductoId { get; set; }
            public string NombreProducto { get; set; } = string.Empty;
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal SubTotal => Cantidad * PrecioUnitario;
        }

        private readonly List<ProductoItem> _inventario = new();
        private readonly List<DetalleVentaViewModel> _detalles = new();

        public VentasForm()
        {
            InitializeComponent();
        }

        private void VentasForm_Load(object? sender, EventArgs e)
        {
            CargarCatalogos();
            ActualizarGrid();
        }

        private void CargarCatalogos()
        {
            cmbClient.Items.Clear();
            cmbClient.Items.AddRange(new object[]
            {
                "María Fernández López (3-3333-3333)",
                "Comercial El Sol S.A. (4-4444-4444)",
                "Jorge Castro Méndez (5-5555-5555)"
            });
            cmbClient.SelectedIndex = 0;

            _inventario.Clear();
            _inventario.AddRange(new[]
            {
                new ProductoItem { Id = 1, Nombre = "Laptop Dell Inspiron 15", PrecioVenta = 750.00m, StockDisponible = 15 },
                new ProductoItem { Id = 2, Nombre = "Mouse Inalámbrico Logitech M185", PrecioVenta = 15.00m, StockDisponible = 40 },
                new ProductoItem { Id = 3, Nombre = "Teclado Mecánico RGB", PrecioVenta = 45.00m, StockDisponible = 20 },
                new ProductoItem { Id = 4, Nombre = "Monitor Samsung 24 pulg FHD", PrecioVenta = 140.00m, StockDisponible = 10 },
                new ProductoItem { Id = 5, Nombre = "Café Molido Premium 500g", PrecioVenta = 7.50m, StockDisponible = 50 }
            });

            cmbProduct.Items.Clear();
            foreach (var prod in _inventario)
            {
                cmbProduct.Items.Add(prod);
            }
            if (cmbProduct.Items.Count > 0)
            {
                cmbProduct.SelectedIndex = 0;
            }
        }

        private void cmbProduct_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem is not ProductoItem prod) return;

            nudUnitPrice.Value = prod.PrecioVenta;
            lblStockInfo.Text = $"Stock disponible: {prod.StockDisponible}";
            ActualizarPreviewSubTotal();
        }

        private void nudQuantity_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarPreviewSubTotal();
        }

        private void ActualizarPreviewSubTotal()
        {
            decimal subTotal = nudQuantity.Value * nudUnitPrice.Value;
            lblLineSubTotal.Text = $"Subtotal línea: {subTotal:C2}";
        }

        private void btnAddDetail_Click(object? sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem is not ProductoItem prod) return;

            int cantidad = (int)nudQuantity.Value;
            var existente = _detalles.FirstOrDefault(d => d.ProductoId == prod.Id);
            int totalSolicitado = (existente?.Cantidad ?? 0) + cantidad;

            if (totalSolicitado > prod.StockDisponible)
            {
                MessageBox.Show(
                    $"No se puede vender una cantidad superior al stock disponible ({prod.StockDisponible} unidades).",
                    "Stock Insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (existente != null)
            {
                existente.Cantidad = totalSolicitado;
            }
            else
            {
                _detalles.Add(new DetalleVentaViewModel
                {
                    ProductoId = prod.Id,
                    NombreProducto = prod.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = prod.PrecioVenta
                });
            }

            nudQuantity.Value = 1;
            ActualizarGrid();
        }

        private void btnRemoveDetail_Click(object? sender, EventArgs e)
        {
            if (dgvSaleDetails.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto del detalle para quitarlo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int prodId = Convert.ToInt32(dgvSaleDetails.SelectedRows[0].Cells["colId"].Value);
            var item = _detalles.FirstOrDefault(d => d.ProductoId == prodId);
            if (item != null)
            {
                _detalles.Remove(item);
                ActualizarGrid();
            }
        }

        private void ActualizarGrid()
        {
            dgvSaleDetails.Rows.Clear();

            foreach (var d in _detalles)
            {
                dgvSaleDetails.Rows.Add(
                    d.ProductoId,
                    d.NombreProducto,
                    d.Cantidad,
                    d.PrecioUnitario.ToString("C2"),
                    d.SubTotal.ToString("C2")
                );
            }

            decimal totalVenta = _detalles.Sum(d => d.SubTotal);
            lblTotalSale.Text = $"Total Venta: {totalVenta:C2}";
        }

        private void btnConfirmSale_Click(object? sender, EventArgs e)
        {
            if (_detalles.Count == 0)
            {
                MessageBox.Show("Una venta debe tener al menos un detalle.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var detalle in _detalles)
            {
                var prod = _inventario.FirstOrDefault(p => p.Id == detalle.ProductoId);
                if (prod != null)
                {
                    prod.StockDisponible -= detalle.Cantidad;
                }
            }

            decimal total = _detalles.Sum(d => d.SubTotal);
            MessageBox.Show(
                $"Venta registrada correctamente para '{cmbClient.SelectedItem}'.\nTotal facturado: {total:C2}",
                "Venta Confirmada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _detalles.Clear();
            cmbProduct_SelectedIndexChanged(null, EventArgs.Empty);
            ActualizarGrid();
        }

        private void btnNewSale_Click(object? sender, EventArgs e)
        {
            _detalles.Clear();
            nudQuantity.Value = 1;
            ActualizarGrid();
        }
    }
}
