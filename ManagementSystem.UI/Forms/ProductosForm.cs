using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class ProductosForm : Form
    {
        private readonly IServicioProductos _servicioProductos;
        private readonly IServicioCategorias _servicioCategorias;
        private List<Producto> _productos = new();
        private List<Categoria> _categorias = new();
        private bool _modoMemoriaLocal;

        public ProductosForm()
        {
            InitializeComponent();
            _servicioProductos = new ServicioProductos();
            _servicioCategorias = new ServicioCategorias();
        }

        private void ProductosForm_Load(object? sender, EventArgs e)
        {
            CargarCategorias();
            CargarProductos();
            ActualizarGrid();
        }

        private void CargarCategorias()
        {
            cmbCategory.Items.Clear();
            try
            {
                _categorias = _servicioCategorias.ObtenerActivas();
                if (_categorias.Count == 0)
                {
                    _categorias = _servicioCategorias.ObtenerTodas();
                }
            }
            catch
            {
                _categorias = new List<Categoria>
                {
                    new Categoria { CategoriaId = 1, Nombre = "Electrónica y Cómputo" },
                    new Categoria { CategoriaId = 2, Nombre = "Bebidas y Alimentos" },
                    new Categoria { CategoriaId = 3, Nombre = "Papelería y Oficina" },
                    new Categoria { CategoriaId = 4, Nombre = "Limpieza y Hogar" }
                };
            }

            foreach (var categoria in _categorias)
            {
                cmbCategory.Items.Add(categoria);
            }

            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void CargarProductos()
        {
            try
            {
                _productos = _servicioProductos.ObtenerTodos();
                _modoMemoriaLocal = false;
            }
            catch
            {
                _modoMemoriaLocal = true;
                if (_productos.Count == 0)
                {
                    CargarDatosDePrueba();
                }
            }
        }

        private void CargarDatosDePrueba()
        {
            _productos.Clear();
            _productos.AddRange(new[]
            {
                new Producto { ProductoId = 1, Nombre = "Laptop Dell Inspiron 15", CategoriaId = 1, Categoria = new Categoria { CategoriaId = 1, Nombre = "Electrónica y Cómputo" }, PrecioCompra = 550.00m, PrecioVenta = 750.00m, StockActual = 15, StockMinimo = 3, Descripcion = "Intel Core i5, 16GB RAM, SSD 512GB", Activo = true },
                new Producto { ProductoId = 2, Nombre = "Mouse Inalámbrico Logitech M185", CategoriaId = 1, Categoria = new Categoria { CategoriaId = 1, Nombre = "Electrónica y Cómputo" }, PrecioCompra = 8.50m, PrecioVenta = 15.00m, StockActual = 40, StockMinimo = 5, Descripcion = "Sensor óptico 2.4GHz", Activo = true },
                new Producto { ProductoId = 3, Nombre = "Teclado Mecánico RGB", CategoriaId = 1, Categoria = new Categoria { CategoriaId = 1, Nombre = "Electrónica y Cómputo" }, PrecioCompra = 25.00m, PrecioVenta = 45.00m, StockActual = 20, StockMinimo = 4, Descripcion = "Switches Red", Activo = true },
                new Producto { ProductoId = 4, Nombre = "Monitor Samsung 24 pulg FHD", CategoriaId = 1, Categoria = new Categoria { CategoriaId = 1, Nombre = "Electrónica y Cómputo" }, PrecioCompra = 95.00m, PrecioVenta = 140.00m, StockActual = 10, StockMinimo = 2, Descripcion = "Panel IPS 75Hz", Activo = true },
                new Producto { ProductoId = 5, Nombre = "Café Molido Premium 500g", CategoriaId = 2, Categoria = new Categoria { CategoriaId = 2, Nombre = "Bebidas y Alimentos" }, PrecioCompra = 4.00m, PrecioVenta = 7.50m, StockActual = 50, StockMinimo = 10, Descripcion = "Tueste medio artesanal", Activo = true },
                new Producto { ProductoId = 6, Nombre = "Resma Papel Bond A4 75g", CategoriaId = 3, Categoria = new Categoria { CategoriaId = 3, Nombre = "Papelería y Oficina" }, PrecioCompra = 3.50m, PrecioVenta = 6.00m, StockActual = 100, StockMinimo = 20, Descripcion = "500 hojas blancas", Activo = true },
                new Producto { ProductoId = 7, Nombre = "Alcohol en Gel 1 Litro", CategoriaId = 4, Categoria = new Categoria { CategoriaId = 4, Nombre = "Limpieza y Hogar" }, PrecioCompra = 2.50m, PrecioVenta = 4.75m, StockActual = 30, StockMinimo = 5, Descripcion = "Antiséptico 70%", Activo = true }
            });
        }

        private void ActualizarGrid(List<Producto>? lista = null)
        {
            dgvProducts.Rows.Clear();
            var datos = lista ?? _productos;

            foreach (var producto in datos)
            {
                string nombreCategoria = producto.Categoria?.Nombre
                    ?? _categorias.FirstOrDefault(c => c.CategoriaId == producto.CategoriaId)?.Nombre
                    ?? "General";

                int indiceFila = dgvProducts.Rows.Add(
                    producto.ProductoId,
                    producto.Nombre,
                    nombreCategoria,
                    producto.PrecioCompra.ToString("C2"),
                    producto.PrecioVenta.ToString("C2"),
                    producto.StockActual,
                    producto.StockMinimo,
                    producto.Activo ? "Activo" : "Inactivo"
                );

                if (producto.TieneStockBajo())
                {
                    dgvProducts.Rows[indiceFila].Cells["colStock"].Style.ForeColor = Color.Red;
                    dgvProducts.Rows[indiceFila].Cells["colStock"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }

                if (!producto.Activo)
                {
                    dgvProducts.Rows[indiceFila].DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
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

            var fila = dgvProducts.SelectedRows[0];
            if (fila.Cells["colId"].Value == null) return;

            int id = Convert.ToInt32(fila.Cells["colId"].Value);
            var producto = _productos.FirstOrDefault(p => p.ProductoId == id);
            if (producto == null) return;

            txtProductId.Text = producto.ProductoId.ToString();
            txtProductName.Text = producto.Nombre;

            var categoriaItem = _categorias.FirstOrDefault(c => c.CategoriaId == producto.CategoriaId);
            if (categoriaItem != null)
            {
                cmbCategory.SelectedItem = categoriaItem;
            }

            nudPurchasePrice.Value = producto.PrecioCompra;
            nudSalePrice.Value = producto.PrecioVenta;
            nudCurrentStock.Value = producto.StockActual;
            nudMinimumStock.Value = producto.StockMinimo;
            txtDescription.Text = producto.Descripcion ?? string.Empty;
            chkActive.Checked = producto.Activo;
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
            try
            {
                int id = int.Parse(txtProductId.Text);
                var categoriaSeleccionada = cmbCategory.SelectedItem as Categoria;
                int categoriaId = categoriaSeleccionada?.CategoriaId ?? 1;

                var entidadProducto = new Producto
                {
                    ProductoId = id,
                    Nombre = txtProductName.Text.Trim(),
                    CategoriaId = categoriaId,
                    Categoria = categoriaSeleccionada,
                    PrecioCompra = nudPurchasePrice.Value,
                    PrecioVenta = nudSalePrice.Value,
                    StockActual = (int)nudCurrentStock.Value,
                    StockMinimo = (int)nudMinimumStock.Value,
                    Descripcion = txtDescription.Text.Trim(),
                    Activo = chkActive.Checked
                };

                if (!_servicioProductos.ValidarProducto(entidadProducto, out List<string> errores))
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errores), "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_modoMemoriaLocal)
                {
                    if (id == 0)
                    {
                        _servicioProductos.AgregarProducto(entidadProducto);
                        MessageBox.Show("Producto registrado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _servicioProductos.ActualizarProducto(entidadProducto);
                        MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    CargarProductos();
                }
                else
                {
                    if (id == 0)
                    {
                        entidadProducto.ProductoId = _productos.Any() ? _productos.Max(p => p.ProductoId) + 1 : 1;
                        _productos.Add(entidadProducto);
                        MessageBox.Show("Producto registrado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var actual = _productos.FirstOrDefault(p => p.ProductoId == id);
                        if (actual != null)
                        {
                            actual.Nombre = entidadProducto.Nombre;
                            actual.CategoriaId = entidadProducto.CategoriaId;
                            actual.Categoria = entidadProducto.Categoria;
                            actual.PrecioCompra = entidadProducto.PrecioCompra;
                            actual.PrecioVenta = entidadProducto.PrecioVenta;
                            actual.StockActual = entidadProducto.StockActual;
                            actual.StockMinimo = entidadProducto.StockMinimo;
                            actual.Descripcion = entidadProducto.Descripcion;
                            actual.Activo = entidadProducto.Activo;

                            MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtProductId.Text);
                if (id == 0)
                {
                    MessageBox.Show("Seleccione un producto para inactivar o activar.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var producto = _productos.FirstOrDefault(p => p.ProductoId == id);
                if (producto == null) return;

                string accion = producto.Activo ? "Inactivo" : "Activo";
                var confirmacion = MessageBox.Show(
                    $"¿Desea cambiar el estado de '{producto.Nombre}' a {accion}?",
                    "Eliminación Lógica",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    bool nuevoEstado = !producto.Activo;
                    if (!_modoMemoriaLocal)
                    {
                        _servicioProductos.CambiarEstadoProducto(id, nuevoEstado);
                        CargarProductos();
                    }
                    else
                    {
                        producto.Activo = nuevoEstado;
                    }
                    ActualizarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar el estado del producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string consulta = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(consulta))
            {
                CargarProductos();
                ActualizarGrid();
                return;
            }

            if (!_modoMemoriaLocal)
            {
                var filtradosServicio = _servicioProductos.BuscarProductos(consulta);
                ActualizarGrid(filtradosServicio);
            }
            else
            {
                var filtrados = _productos
                    .Where(p => p.Nombre.Contains(consulta, StringComparison.OrdinalIgnoreCase) ||
                                (p.Descripcion != null && p.Descripcion.Contains(consulta, StringComparison.OrdinalIgnoreCase)) ||
                                (p.Categoria != null && p.Categoria.Nombre.Contains(consulta, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                ActualizarGrid(filtrados);
            }
        }

        private void btnResetFilter_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            CargarProductos();
            ActualizarGrid();
        }
    }
}
