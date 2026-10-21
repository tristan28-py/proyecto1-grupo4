using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class CategoriasForm : Form
    {
        private class CategoriaViewModel
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
            public DateTime FechaCreacion { get; set; } = DateTime.Now;
            public bool Activo { get; set; } = true;
        }

        private readonly List<CategoriaViewModel> _categorias = new();

        public CategoriasForm()
        {
            InitializeComponent();
        }

        private void CategoriasForm_Load(object? sender, EventArgs e)
        {
            cmbStatusFilter.SelectedIndex = 0; // Todos
            CargarDatosDePrueba();
            ActualizarGrid();
        }

        private void CargarDatosDePrueba()
        {
            _categorias.Clear();
            _categorias.AddRange(new[]
            {
                new CategoriaViewModel { Id = 1, Nombre = "Electrónica y Cómputo", Descripcion = "Laptops, accesorios, periféricos y monitores", FechaCreacion = DateTime.Now.AddDays(-30), Activo = true },
                new CategoriaViewModel { Id = 2, Nombre = "Bebidas y Alimentos", Descripcion = "Snacks, confitería, café y refrescos", FechaCreacion = DateTime.Now.AddDays(-25), Activo = true },
                new CategoriaViewModel { Id = 3, Nombre = "Papelería y Oficina", Descripcion = "Cuadernos, bolígrafos, resmas y papelería general", FechaCreacion = DateTime.Now.AddDays(-20), Activo = true },
                new CategoriaViewModel { Id = 4, Nombre = "Limpieza y Hogar", Descripcion = "Artículos de higiene, alcohol en gel y detergentes", FechaCreacion = DateTime.Now.AddDays(-15), Activo = true },
                new CategoriaViewModel { Id = 5, Nombre = "Mobiliario Descontinuado", Descripcion = "Línea de escritorios antigua", FechaCreacion = DateTime.Now.AddDays(-60), Activo = false }
            });
        }

        private void ActualizarGrid()
        {
            dgvCategories.Rows.Clear();

            string query = txtSearch.Text.Trim().ToLower();
            int statusIndex = cmbStatusFilter.SelectedIndex;

            // Filtro LINQ (Páginas 2 y 4 del PDF: Consulta por estado y búsqueda por texto)
            var filtrados = _categorias.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                filtrados = filtrados.Where(c => c.Nombre.ToLower().Contains(query) ||
                                                 c.Descripcion.ToLower().Contains(query));
            }

            if (statusIndex == 1) // Solo Activos
            {
                filtrados = filtrados.Where(c => c.Activo);
            }
            else if (statusIndex == 2) // Solo Inactivos
            {
                filtrados = filtrados.Where(c => !c.Activo);
            }

            var lista = filtrados.OrderBy(c => c.Nombre).ToList();

            foreach (var c in lista)
            {
                int rowIndex = dgvCategories.Rows.Add(
                    c.Id,
                    c.Nombre,
                    c.Descripcion,
                    c.FechaCreacion.ToString("dd/MM/yyyy"),
                    c.Activo ? "Activo" : "Inactivo"
                );

                if (!c.Activo)
                {
                    dgvCategories.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
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

        private void dgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0) return;

            var row = dgvCategories.SelectedRows[0];
            if (row.Cells["colId"].Value == null) return;

            int id = Convert.ToInt32(row.Cells["colId"].Value);
            var cat = _categorias.FirstOrDefault(c => c.Id == id);
            if (cat == null) return;

            txtCategoryId.Text = cat.Id.ToString();
            txtCategoryName.Text = cat.Nombre;
            txtDescription.Text = cat.Descripcion;
            chkActive.Checked = cat.Activo;
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
            string nombre = txtCategoryName.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre de la categoría es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return;
            }

            int id = int.Parse(txtCategoryId.Text);

            if (id == 0)
            {
                // Nueva categoría
                int nuevoId = _categorias.Any() ? _categorias.Max(c => c.Id) + 1 : 1;
                _categorias.Add(new CategoriaViewModel
                {
                    Id = nuevoId,
                    Nombre = nombre,
                    Descripcion = txtDescription.Text.Trim(),
                    FechaCreacion = DateTime.Now,
                    Activo = chkActive.Checked
                });

                MessageBox.Show("Categoría agregada exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Editar existente
                var cat = _categorias.FirstOrDefault(c => c.Id == id);
                if (cat != null)
                {
                    cat.Nombre = nombre;
                    cat.Descripcion = txtDescription.Text.Trim();
                    cat.Activo = chkActive.Checked;

                    MessageBox.Show("Categoría actualizada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            ActualizarGrid();
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            if (id == 0)
            {
                MessageBox.Show("Seleccione una categoría para cambiar su estado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cat = _categorias.FirstOrDefault(c => c.Id == id);
            if (cat == null) return;

            string accion = cat.Activo ? "inactivar" : "activar";
            var result = MessageBox.Show(
                $"¿Desea {accion} la categoría '{cat.Nombre}'?",
                "Estado de Categoría",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                cat.Activo = !cat.Activo;
                ActualizarGrid();
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
