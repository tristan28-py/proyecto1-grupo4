using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class ProductosForm : Form
    {
        private class ProductoViewModel
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Categoria { get; set; } = string.Empty;
            public decimal PrecioCompra { get; set; }
            public decimal PrecioVenta { get; set; }
            public int StockActual { get; set; }
            public int StockMinimo { get; set; }
            public string Descripcion { get; set; } = string.Empty;
            public bool Activo { get; set; } = true;
        }

        private readonly List<ProductoViewModel> _productos = new();

        public ProductosForm()
        {
            InitializeComponent();
        }

        private void ProductosForm_Load(object? sender, EventArgs e)
        {
            CargarCategorias();
            CargarDatosDePrueba();
            ActualizarGrid();
        }

        private void CargarCategorias()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("Electrónica y Cómputo");
            cmbCategory.Items.Add("Bebidas y Alimentos");
            cmbCategory.Items.Add("Papelería y Oficina");
            cmbCategory.Items.Add("Limpieza y Hogar");
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private void CargarDatosDePrueba()
        {
            _productos.Clear();
            _productos.AddRange(new[]
            {
                new ProductoViewModel { Id = 1, Nombre = "Laptop Dell Inspiron 15", Categoria = "Electrónica y Cómputo", PrecioCompra = 550.00m, PrecioVenta = 750.00m, StockActual = 15, StockMinimo = 3, Descripcion = "Intel Core i5, 16GB RAM, SSD 512GB", Activo = true },
                new ProductoViewModel { Id = 2, Nombre = "Mouse Inalámbrico Logitech M185", Categoria = "Electrónica y Cómputo", PrecioCompra = 8.50m, PrecioVenta = 15.00m, StockActual = 40, StockMinimo = 5, Descripcion = "Sensor óptico 2.4GHz", Activo = true },
                new ProductoViewModel { Id = 3, Nombre = "Teclado Mecánico RGB", Categoria = "Electrónica y Cómputo", PrecioCompra = 25.00m, PrecioVenta = 45.00m, StockActual = 20, StockMinimo = 4, Descripcion = "Switches Red", Activo = true },
                new ProductoViewModel { Id = 4, Nombre = "Monitor Samsung 24 pulg FHD", Categoria = "Electrónica y Cómputo", PrecioCompra = 95.00m, PrecioVenta = 140.00m, StockActual = 10, StockMinimo = 2, Descripcion = "Panel IPS 75Hz", Activo = true },
                new ProductoViewModel { Id = 5, Nombre = "Café Molido Premium 500g", Categoria = "Bebidas y Alimentos", PrecioCompra = 4.00m, PrecioVenta = 7.50m, StockActual = 50, StockMinimo = 10, Descripcion = "Tueste medio artesanal", Activo = true },
                new ProductoViewModel { Id = 6, Nombre = "Resma Papel Bond A4 75g", Categoria = "Papelería y Oficina", PrecioCompra = 3.50m, PrecioVenta = 6.00m, StockActual = 100, StockMinimo = 20, Descripcion = "500 hojas blancas", Activo = true },
                new ProductoViewModel { Id = 7, Nombre = "Alcohol en Gel 1 Litro", Categoria = "Limpieza y Hogar", PrecioCompra = 2.50m, PrecioVenta = 4.75m, StockActual = 30, StockMinimo = 5, Descripcion = "Antiséptico 70%", Activo = true }
            });
        }

        private void ActualizarGrid(List<ProductoViewModel>? lista = null)
        {
            dgvProducts.Rows.Clear();
            var datos = lista ?? _productos;

            foreach (var p in datos)
            {
                int rowIndex = dgvProducts.Rows.Add(
                    p.Id,
                    p.Nombre,
                    p.Categoria,
                    p.PrecioCompra.ToString("C2"),
                    p.PrecioVenta.ToString("C2"),
                    p.StockActual,
                    p.StockMinimo,
                    p.Activo ? "Activo" : "Inactivo"
                );

                if (p.StockActual <= p.StockMinimo)
                {
                    dgvProducts.Rows[rowIndex].Cells["colStock"].Style.ForeColor = Color.Red;
                    dgvProducts.Rows[rowIndex].Cells["colStock"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }

                if (!p.Activo)
                {
                    dgvProducts.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
                }
            }

            if (dgvProducts.Rows.Count > 0)
            {
                dgvProducts.Rows[0].Selected = true;
            }
            else
            {
                LimpiarFormulario();
            }
        }

        private void dgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            var row = dgvProducts.SelectedRows[0];
            if (row.Cells["colId"].Value == null) return;

            int id = Convert.ToInt32(row.Cells["colId"].Value);
            var prod = _productos.FirstOrDefault(p => p.Id == id);
            if (prod == null) return;

            txtProductId.Text = prod.Id.ToString();
            txtProductName.Text = prod.Nombre;
            cmbCategory.SelectedItem = prod.Categoria;
            nudPurchasePrice.Value = prod.PrecioCompra;
            nudSalePrice.Value = prod.PrecioVenta;
            nudCurrentStock.Value = prod.StockActual;
            nudMinimumStock.Value = prod.StockMinimo;
            txtDescription.Text = prod.Descripcion;
            chkActive.Checked = prod.Activo;
        }

        private void btnNew_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtProductId.Text = "0";
            txtProductName.Clear();
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            nudPurchasePrice.Value = 0;
            nudSalePrice.Value = 1;
            nudCurrentStock.Value = 0;
            nudMinimumStock.Value = 1;
            txtDescription.Clear();
            chkActive.Checked = true;
            txtProductName.Focus();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            string nombre = txtProductName.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            if (nudSalePrice.Value <= 0)
            {
                MessageBox.Show("No se puede registrar un producto con precio menor o igual a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSalePrice.Focus();
                return;
            }

            if (nudCurrentStock.Value < 0)
            {
                MessageBox.Show("No se puede registrar una cantidad negativa de inventario.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCurrentStock.Focus();
                return;
            }

            int id = int.Parse(txtProductId.Text);

            if (id == 0)
            {
                int nuevoId = _productos.Any() ? _productos.Max(p => p.Id) + 1 : 1;
                _productos.Add(new ProductoViewModel
                {
                    Id = nuevoId,
                    Nombre = nombre,
                    Categoria = cmbCategory.SelectedItem?.ToString() ?? "General",
                    PrecioCompra = nudPurchasePrice.Value,
                    PrecioVenta = nudSalePrice.Value,
                    StockActual = (int)nudCurrentStock.Value,
                    StockMinimo = (int)nudMinimumStock.Value,
                    Descripcion = txtDescription.Text.Trim(),
                    Activo = chkActive.Checked
                });

                MessageBox.Show("Producto registrado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var actual = _productos.FirstOrDefault(p => p.Id == id);
                if (actual != null)
                {
                    actual.Nombre = nombre;
                    actual.Categoria = cmbCategory.SelectedItem?.ToString() ?? "General";
                    actual.PrecioCompra = nudPurchasePrice.Value;
                    actual.PrecioVenta = nudSalePrice.Value;
                    actual.StockActual = (int)nudCurrentStock.Value;
                    actual.StockMinimo = (int)nudMinimumStock.Value;
                    actual.Descripcion = txtDescription.Text.Trim();
                    actual.Activo = chkActive.Checked;

                    MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            ActualizarGrid();
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            int id = int.Parse(txtProductId.Text);
            if (id == 0)
            {
                MessageBox.Show("Seleccione un producto para inactivar o eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var prod = _productos.FirstOrDefault(p => p.Id == id);
            if (prod == null) return;

            var result = MessageBox.Show(
                $"¿Desea cambiar el estado de '{prod.Nombre}' a Inactivo?",
                "Eliminación Lógica",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                prod.Activo = !prod.Activo;
                ActualizarGrid();
            }
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            EjecutarBusqueda();
        }

        private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                EjecutarBusqueda();
            }
        }

        private void EjecutarBusqueda()
        {
            string query = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(query))
            {
                ActualizarGrid();
                return;
            }

            var filtrados = _productos
                .Where(p => p.Nombre.ToLower().Contains(query) ||
                            p.Descripcion.ToLower().Contains(query) ||
                            p.Categoria.ToLower().Contains(query))
                .ToList();

            ActualizarGrid(filtrados);
        }

        private void btnResetFilter_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            ActualizarGrid();
        }
    }
}
