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
    public partial class CategoriasForm : Form
    {
        private readonly IServicioCategorias _servicioCategorias;
        private List<Categoria> _categorias = new();
        private bool _modoMemoriaLocal;

        public CategoriasForm()
        {
            InitializeComponent();
            _servicioCategorias = new ServicioCategorias();
        }

        private void CategoriasForm_Load(object? sender, EventArgs e)
        {
            cmbStatusFilter.SelectedIndex = 0;
            CargarCategoriasDesdeServicio();
            ActualizarGrid();
        }

        private void CargarCategoriasDesdeServicio()
        {
            try
            {
                _categorias = _servicioCategorias.ObtenerTodas();
                _modoMemoriaLocal = false;
            }
            catch
            {
                _modoMemoriaLocal = true;
                if (_categorias.Count == 0)
                {
                    CargarDatosDePrueba();
                }
            }
        }

        private void CargarDatosDePrueba()
        {
            _categorias.Clear();
            _categorias.AddRange(new[]
            {
                new Categoria { CategoriaId = 1, Nombre = "Electrónica y Cómputo", Descripcion = "Laptops, accesorios, periféricos y monitores", FechaCreacion = DateTime.Now.AddDays(-30), Activo = true },
                new Categoria { CategoriaId = 2, Nombre = "Bebidas y Alimentos", Descripcion = "Snacks, confitería, café y refrescos", FechaCreacion = DateTime.Now.AddDays(-25), Activo = true },
                new Categoria { CategoriaId = 3, Nombre = "Papelería y Oficina", Descripcion = "Cuadernos, bolígrafos, resmas y papelería general", FechaCreacion = DateTime.Now.AddDays(-20), Activo = true },
                new Categoria { CategoriaId = 4, Nombre = "Limpieza y Hogar", Descripcion = "Artículos de higiene, alcohol en gel y detergentes", FechaCreacion = DateTime.Now.AddDays(-15), Activo = true }
            });
        }

        private void ActualizarGrid()
        {
            dgvCategories.Rows.Clear();

            string textoBusqueda = txtSearch.Text.Trim();
            int indiceEstado = cmbStatusFilter.SelectedIndex;

            List<Categoria> listaFiltrada;
            if (!_modoMemoriaLocal)
            {
                try
                {
                    listaFiltrada = _servicioCategorias.FiltrarCategorias(textoBusqueda, indiceEstado);
                    _categorias = _servicioCategorias.ObtenerTodas();
                }
                catch
                {
                    _modoMemoriaLocal = true;
                    listaFiltrada = FiltrarEnMemoria(textoBusqueda, indiceEstado);
                }
            }
            else
            {
                listaFiltrada = FiltrarEnMemoria(textoBusqueda, indiceEstado);
            }

            foreach (var categoria in listaFiltrada)
            {
                int indiceFila = dgvCategories.Rows.Add(
                    categoria.CategoriaId,
                    categoria.Nombre,
                    categoria.Descripcion ?? string.Empty,
                    categoria.FechaCreacion.ToString("dd/MM/yyyy"),
                    categoria.Activo ? "Activo" : "Inactivo"
                );

                if (!categoria.Activo)
                {
                    dgvCategories.Rows[indiceFila].DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
                }
            }

            if (dgvCategories.Rows.Count > 0)
            {
                dgvCategories.Rows[0].Selected = true;
            }
            else
            {
                LimpiarFormulario();
            }
        }

        private List<Categoria> FiltrarEnMemoria(string textoBusqueda, int indiceEstado)
        {
            IEnumerable<Categoria> filtrados = _categorias;

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                filtrados = filtrados.Where(c =>
                    c.Nombre.Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                    (c.Descripcion != null && c.Descripcion.Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase)));
            }

            if (indiceEstado == 1)
            {
                filtrados = filtrados.Where(c => c.Activo);
            }
            else if (indiceEstado == 2)
            {
                filtrados = filtrados.Where(c => !c.Activo);
            }

            return filtrados.OrderBy(c => c.Nombre).ToList();
        }

        private void dgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0) return;

            var fila = dgvCategories.SelectedRows[0];
            if (fila.Cells["colId"].Value == null) return;

            int id = Convert.ToInt32(fila.Cells["colId"].Value);
            var categoria = _categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null) return;

            txtCategoryId.Text = categoria.CategoriaId.ToString();
            txtCategoryName.Text = categoria.Nombre;
            txtDescription.Text = categoria.Descripcion ?? string.Empty;
            chkActive.Checked = categoria.Activo;
        }

        private void btnNew_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtCategoryId.Text = "0";
            txtCategoryName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            txtCategoryName.Focus();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtCategoryName.Text.Trim();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("El nombre de la categoría es obligatorio.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategoryName.Focus();
                    return;
                }

                int id = int.Parse(txtCategoryId.Text);
                var entidad = new Categoria
                {
                    CategoriaId = id,
                    Nombre = nombre,
                    Descripcion = txtDescription.Text.Trim(),
                    FechaCreacion = DateTime.Now,
                    Activo = chkActive.Checked
                };

                if (!_modoMemoriaLocal)
                {
                    if (id == 0)
                    {
                        _servicioCategorias.AgregarCategoria(entidad);
                        MessageBox.Show("Categoría agregada exitosamente en la base de datos.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _servicioCategorias.ActualizarCategoria(entidad);
                        MessageBox.Show("Categoría actualizada correctamente en la base de datos.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (id == 0)
                    {
                        entidad.CategoriaId = _categorias.Any() ? _categorias.Max(c => c.CategoriaId) + 1 : 1;
                        _categorias.Add(entidad);
                        MessageBox.Show("Categoría agregada exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var existente = _categorias.FirstOrDefault(c => c.CategoriaId == id);
                        if (existente != null)
                        {
                            existente.Nombre = entidad.Nombre;
                            existente.Descripcion = entidad.Descripcion;
                            existente.Activo = entidad.Activo;
                            MessageBox.Show("Categoría actualizada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la categoría: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtCategoryId.Text);
                if (id == 0)
                {
                    MessageBox.Show("Seleccione una categoría para cambiar su estado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var categoria = _categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria == null) return;

                string accion = categoria.Activo ? "inactivar" : "activar";
                var confirmacion = MessageBox.Show(
                    $"¿Desea {accion} la categoría '{categoria.Nombre}'?",
                    "Estado de Categoría",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    bool nuevoEstado = !categoria.Activo;
                    if (!_modoMemoriaLocal)
                    {
                        _servicioCategorias.CambiarEstadoCategoria(id, nuevoEstado);
                    }
                    else
                    {
                        categoria.Activo = nuevoEstado;
                    }
                    ActualizarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar el estado de la categoría: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            ActualizarGrid();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarGrid();
        }
    }
}
