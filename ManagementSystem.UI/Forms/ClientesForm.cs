using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class ClientesForm : Form
    {
        private class PersonaViewModel
        {
            public int Id { get; set; }
            public string Identificacion { get; set; } = string.Empty;
            public string NombreCompleto { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public string Tipo { get; set; } = "Cliente";
            public decimal MontoAsignado { get; set; }
            public bool Activo { get; set; } = true;
        }

        private readonly List<PersonaViewModel> _personas = new();

        public ClientesForm()
        {
            InitializeComponent();
        }

        private void ClientesForm_Load(object? sender, EventArgs e)
        {
            cmbTypeFilter.SelectedIndex = 0;
            cmbPersonType.SelectedIndex = 0;
            CargarDatosDePrueba();
            ActualizarGrid();
        }

        private void CargarDatosDePrueba()
        {
            _personas.Clear();
            _personas.AddRange(new[]
            {
                new PersonaViewModel { Id = 1, Identificacion = "1-1111-1111", NombreCompleto = "Carlos Ramírez Soto", Email = "carlos@sistema.com", Telefono = "8888-1111", Tipo = "Usuario", MontoAsignado = 850.00m, Activo = true },
                new PersonaViewModel { Id = 2, Identificacion = "2-2222-2222", NombreCompleto = "Ana Gómez Vargas", Email = "ana@sistema.com", Telefono = "8888-2222", Tipo = "Empleado", MontoAsignado = 620.00m, Activo = true },
                new PersonaViewModel { Id = 3, Identificacion = "3-3333-3333", NombreCompleto = "María Fernández López", Email = "maria@correo.com", Telefono = "8888-3333", Tipo = "Cliente", MontoAsignado = 1500.00m, Activo = true },
                new PersonaViewModel { Id = 4, Identificacion = "4-4444-4444", NombreCompleto = "Comercial El Sol S.A.", Email = "ventas@elsol.com", Telefono = "2222-4444", Tipo = "Cliente", MontoAsignado = 5000.00m, Activo = true },
                new PersonaViewModel { Id = 5, Identificacion = "5-5555-5555", NombreCompleto = "Jorge Castro Méndez", Email = "jorge@correo.com", Telefono = "8777-5555", Tipo = "Cliente", MontoAsignado = 500.00m, Activo = false }
            });
        }

        private void ActualizarGrid()
        {
            dgvClients.Rows.Clear();

            string query = txtSearch.Text.Trim().ToLower();
            string tipoFiltro = cmbTypeFilter.SelectedItem?.ToString() ?? "Todos";

            var filtrados = _personas.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                filtrados = filtrados.Where(p => p.NombreCompleto.ToLower().Contains(query) ||
                                                 p.Identificacion.ToLower().Contains(query));
            }

            if (tipoFiltro != "Todos")
            {
                filtrados = filtrados.Where(p => p.Tipo == tipoFiltro);
            }

            var lista = filtrados.OrderBy(p => p.NombreCompleto).ToList();

            foreach (var p in lista)
            {
                int rowIndex = dgvClients.Rows.Add(
                    p.Id,
                    p.Identificacion,
                    p.NombreCompleto,
                    p.Tipo,
                    p.Telefono,
                    p.MontoAsignado.ToString("C2"),
                    p.Activo ? "Activo" : "Inactivo"
                );

                if (!p.Activo)
                {
                    dgvClients.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
                }
            }

            if (dgvClients.Rows.Count > 0)
            {
                dgvClients.Rows[0].Selected = true;
            }
            else
            {
                LimpiarFormulario();
            }
        }

        private void dgvClients_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0) return;

            var row = dgvClients.SelectedRows[0];
            if (row.Cells["colId"].Value == null) return;

            int id = Convert.ToInt32(row.Cells["colId"].Value);
            var per = _personas.FirstOrDefault(p => p.Id == id);
            if (per == null) return;

            txtClientId.Text = per.Id.ToString();
            txtFullName.Text = per.NombreCompleto;
            txtIdentification.Text = per.Identificacion;
            txtEmail.Text = per.Email;
            txtPhone.Text = per.Telefono;
            cmbPersonType.SelectedItem = per.Tipo;
            nudCreditLimit.Value = per.MontoAsignado;
            chkActive.Checked = per.Activo;
        }

        private void btnNew_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtClientId.Text = "0";
            txtFullName.Clear();
            txtIdentification.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            if (cmbPersonType.Items.Count > 0) cmbPersonType.SelectedIndex = 0;
            nudCreditLimit.Value = 0;
            chkActive.Checked = true;
            txtFullName.Focus();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            string nombre = txtFullName.Text.Trim();
            string cedula = txtIdentification.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre completo es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cedula))
            {
                MessageBox.Show("La identificación es obligatoria.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdentification.Focus();
                return;
            }

            int id = int.Parse(txtClientId.Text);

            if (id == 0)
            {
                int nuevoId = _personas.Any() ? _personas.Max(p => p.Id) + 1 : 1;
                _personas.Add(new PersonaViewModel
                {
                    Id = nuevoId,
                    NombreCompleto = nombre,
                    Identificacion = cedula,
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtPhone.Text.Trim(),
                    Tipo = cmbPersonType.SelectedItem?.ToString() ?? "Cliente",
                    MontoAsignado = nudCreditLimit.Value,
                    Activo = chkActive.Checked
                });

                MessageBox.Show("Registro guardado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var per = _personas.FirstOrDefault(p => p.Id == id);
                if (per != null)
                {
                    per.NombreCompleto = nombre;
                    per.Identificacion = cedula;
                    per.Email = txtEmail.Text.Trim();
                    per.Telefono = txtPhone.Text.Trim();
                    per.Tipo = cmbPersonType.SelectedItem?.ToString() ?? "Cliente";
                    per.MontoAsignado = nudCreditLimit.Value;
                    per.Activo = chkActive.Checked;

                    MessageBox.Show("Registro actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            ActualizarGrid();
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            int id = int.Parse(txtClientId.Text);
            if (id == 0)
            {
                MessageBox.Show("Seleccione un registro para cambiar su estado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var per = _personas.FirstOrDefault(p => p.Id == id);
            if (per == null) return;

            string accion = per.Activo ? "inactivar" : "activar";
            var result = MessageBox.Show(
                $"¿Desea {accion} a '{per.NombreCompleto}'?",
                "Confirmar Estado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                per.Activo = !per.Activo;
                ActualizarGrid();
            }
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            ActualizarGrid();
        }

        private void cmbTypeFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarGrid();
        }
    }
}
