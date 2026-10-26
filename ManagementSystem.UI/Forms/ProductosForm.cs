using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaGestion.Entidades.Entidades;
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

            if (producto.Activo)
            {
                btnDelete.Text = "Eliminar";
                btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                btnDelete.Text = "Reactivar";
                btnDelete.BackColor = Color.FromArgb(16, 185, 129);
            }
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
            btnDelete.Text = "Eliminar";
            btnDelete.BackColor = Color.FromArgb(220, 38, 38);
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
                        int nuevoId = _servicioProductos.AgregarProducto(entidadProducto);
                        txtProductId.Text = nuevoId.ToString();
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
                        txtProductId.Text = entidadProducto.ProductoId.ToString();
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
                    MessageBox.Show("Seleccione un producto de la lista para eliminarlo.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var producto = _productos.FirstOrDefault(p => p.ProductoId == id);
                if (producto == null) return;

                if (producto.Activo)
                {
                    var confirmacion = MessageBox.Show(
                        $"¿Está seguro de que desea eliminar el producto '{producto.Nombre}'?\n\n" +
                        "El registro se mantendrá como Inactivo para preservar el historial de movimientos y ventas.",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmacion == DialogResult.Yes)
                    {
                        if (!_modoMemoriaLocal)
                        {
                            _servicioProductos.EliminarProducto(id);
                            CargarProductos();
                        }
                        else
                        {
                            producto.Activo = false;
                        }

                        ActualizarGrid();
                        LimpiarFormulario();
                        MessageBox.Show("Producto eliminado (marcado como inactivo) correctamente.", "Eliminación Exitosa",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var resultado = MessageBox.Show(
                        $"El producto '{producto.Nombre}' ya se encuentra inactivo.\n\n" +
                        "• [Sí] para Reactivar el producto.\n" +
                        "• [No] para Eliminarlo definitivamente del sistema.\n" +
                        "• [Cancelar] para salir sin cambios.",
                        "Gestión de Producto Inactivo",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        if (!_modoMemoriaLocal)
                        {
                            _servicioProductos.CambiarEstadoProducto(id, true);
                            CargarProductos();
                        }
                        else
                        {
                            producto.Activo = true;
                        }

                        ActualizarGrid();
                        MessageBox.Show("Producto reactivado correctamente.", "Reactivado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (!_modoMemoriaLocal)
                        {
                            _servicioProductos.EliminarProducto(id);
                            CargarProductos();
                        }
                        else
                        {
                            _productos.Remove(producto);
                        }

                        LimpiarFormulario();
                        ActualizarGrid();
                        MessageBox.Show("Producto eliminado definitivamente.", "Eliminado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la eliminación del producto: {ex.Message}", "Error",
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
