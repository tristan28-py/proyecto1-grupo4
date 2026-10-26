using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ManagementSystem.Entities.DTOs;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class ClientesForm : Form
    {
        private readonly IServicioPersonas _servicioPersonas;
        private List<ResumenPersonaDto> _personas = new();
        private bool _modoMemoriaLocal;

        public ClientesForm()
        {
            InitializeComponent();
            _servicioPersonas = new ServicioPersonas();
        }

        private void ClientesForm_Load(object? sender, EventArgs e)
        {
            cmbTypeFilter.SelectedIndex = 0;
            cmbPersonType.SelectedIndex = 0;
            CargarPersonas();
            ActualizarGrid();
        }

        private void CargarPersonas()
        {
            try
            {
                _personas = _servicioPersonas.ObtenerResumenPersonas();
                _modoMemoriaLocal = false;
            }
            catch
            {
                _modoMemoriaLocal = true;
                if (_personas.Count == 0)
                {
                    CargarDatosDePrueba();
                }
            }
        }

        private void CargarDatosDePrueba()
        {
            _personas.Clear();
            _personas.AddRange(new[]
            {
                new ResumenPersonaDto { PersonaId = 1, Identificacion = "1-1520-0345", NombreCompleto = "Carlos Solís Delgado", Email = "carlos.solis@empresa.com", Telefono = "8888-1111", TiposPersona = "Usuario", Salario = 1250000.00m, TieneUsuario = true, EsEmpleado = true, Activo = true },
                new ResumenPersonaDto { PersonaId = 2, Identificacion = "2-0789-0123", NombreCompleto = "Ana María Morales Mora", Email = "ana.morales@empresa.com", Telefono = "8888-2222", TiposPersona = "Empleado", Salario = 550000.00m, EsEmpleado = true, Activo = true },
                new ResumenPersonaDto { PersonaId = 4, Identificacion = "1-0987-0654", NombreCompleto = "Juan Pérez Gómez", Email = "juan.perez@gmail.com", Telefono = "7777-1111", TiposPersona = "Cliente", LimiteCredito = 350000.00m, EsCliente = true, Activo = true },
                new ResumenPersonaDto { PersonaId = 5, Identificacion = "3-101-998877", NombreCompleto = "Empresa Alfa S.A.", Email = "compras@alfa.com", Telefono = "7777-2222", TiposPersona = "Cliente", LimiteCredito = 2500000.00m, EsCliente = true, Activo = true }
            });
        }

        private void ActualizarGrid()
        {
            dgvClients.Rows.Clear();

            string textoBusqueda = txtSearch.Text.Trim();
            string tipoFiltro = cmbTypeFilter.SelectedItem?.ToString() ?? "Todos";

            List<ResumenPersonaDto> lista;
            if (!_modoMemoriaLocal)
            {
                try
                {
                    lista = _servicioPersonas.FiltrarResumenPersonas(textoBusqueda, tipoFiltro);
                    _personas = _servicioPersonas.ObtenerResumenPersonas();
                }
                catch
                {
                    _modoMemoriaLocal = true;
                    lista = FiltrarEnMemoria(textoBusqueda, tipoFiltro);
                }
            }
            else
            {
                lista = FiltrarEnMemoria(textoBusqueda, tipoFiltro);
            }

            foreach (var persona in lista)
            {
                decimal montoMostrado = persona.EsCliente
                    ? (persona.LimiteCredito ?? 0m)
                    : (persona.Salario ?? 0m);

                int indiceFila = dgvClients.Rows.Add(
                    persona.PersonaId,
                    persona.Identificacion,
                    persona.NombreCompleto,
                    persona.TiposPersona,
                    persona.Telefono ?? string.Empty,
                    montoMostrado.ToString("C2"),
                    persona.Activo ? "Activo" : "Inactivo"
                );

                if (!persona.Activo)
                {
                    dgvClients.Rows[indiceFila].DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
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

        private List<ResumenPersonaDto> FiltrarEnMemoria(string textoBusqueda, string tipoFiltro)
        {
            IEnumerable<ResumenPersonaDto> filtrados = _personas;

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                filtrados = filtrados.Where(p =>
                    p.NombreCompleto.Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                    p.Identificacion.Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase));
            }

            if (tipoFiltro != "Todos")
            {
                filtrados = filtrados.Where(p => p.TiposPersona.Equals(tipoFiltro, StringComparison.OrdinalIgnoreCase));
            }

            return filtrados.OrderBy(p => p.NombreCompleto).ToList();
        }

        private void dgvClients_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0) return;

            var fila = dgvClients.SelectedRows[0];
            if (fila.Cells["colId"].Value == null) return;

            int id = Convert.ToInt32(fila.Cells["colId"].Value);
            var persona = _personas.FirstOrDefault(p => p.PersonaId == id);
            if (persona == null) return;

            txtClientId.Text = persona.PersonaId.ToString();
            txtFullName.Text = persona.NombreCompleto;
            txtIdentification.Text = persona.Identificacion;
            txtEmail.Text = persona.Email ?? string.Empty;
            txtPhone.Text = persona.Telefono ?? string.Empty;

            if (cmbPersonType.Items.Contains(persona.TiposPersona))
            {
                cmbPersonType.SelectedItem = persona.TiposPersona;
            }
            else if (cmbPersonType.Items.Count > 0)
            {
                cmbPersonType.SelectedIndex = 0;
            }

            decimal monto = persona.EsCliente
                ? (persona.LimiteCredito ?? 0m)
                : (persona.Salario ?? 0m);
            nudCreditLimit.Value = monto;
            chkActive.Checked = persona.Activo;
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
            try
            {
                string nombre = txtFullName.Text.Trim();
                string identificacion = txtIdentification.Text.Trim();
                string tipoSeleccionado = cmbPersonType.SelectedItem?.ToString() ?? "Cliente";

                if (!_servicioPersonas.ValidarDatosPersona(nombre, identificacion, nudCreditLimit.Value, out List<string> errores))
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errores), "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtClientId.Text);
                var dto = new ResumenPersonaDto
                {
                    PersonaId = id,
                    NombreCompleto = nombre,
                    Identificacion = identificacion,
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtPhone.Text.Trim(),
                    TiposPersona = tipoSeleccionado,
                    EsCliente = tipoSeleccionado == "Cliente",
                    EsEmpleado = tipoSeleccionado == "Empleado" || tipoSeleccionado == "Usuario",
                    TieneUsuario = tipoSeleccionado == "Usuario",
                    LimiteCredito = tipoSeleccionado == "Cliente" ? nudCreditLimit.Value : null,
                    Salario = tipoSeleccionado != "Cliente" ? nudCreditLimit.Value : null,
                    Puesto = tipoSeleccionado != "Cliente" ? $"Colaborador ({tipoSeleccionado})" : null,
                    Activo = chkActive.Checked
                };

                if (!_modoMemoriaLocal)
                {
                    _servicioPersonas.GuardarPersonaSegunTipo(dto);
                    MessageBox.Show("Registro guardado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (id == 0)
                    {
                        dto.PersonaId = _personas.Any() ? _personas.Max(p => p.PersonaId) + 1 : 1;
                        _personas.Add(dto);
                    }
                    else
                    {
                        var existente = _personas.FirstOrDefault(p => p.PersonaId == id);
                        if (existente != null)
                        {
                            existente.NombreCompleto = dto.NombreCompleto;
                            existente.Identificacion = dto.Identificacion;
                            existente.Email = dto.Email;
                            existente.Telefono = dto.Telefono;
                            existente.TiposPersona = dto.TiposPersona;
                            existente.EsCliente = dto.EsCliente;
                            existente.EsEmpleado = dto.EsEmpleado;
                            existente.TieneUsuario = dto.TieneUsuario;
                            existente.LimiteCredito = dto.LimiteCredito;
                            existente.Salario = dto.Salario;
                            existente.Activo = dto.Activo;
                        }
                    }

                    MessageBox.Show("Registro guardado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el registro: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtClientId.Text);
                if (id == 0)
                {
                    MessageBox.Show("Seleccione un registro para cambiar su estado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var persona = _personas.FirstOrDefault(p => p.PersonaId == id);
                if (persona == null) return;

                string accion = persona.Activo ? "inactivar" : "activar";
                var confirmacion = MessageBox.Show(
                    $"¿Desea {accion} a '{persona.NombreCompleto}'?",
                    "Confirmar Estado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    bool nuevoEstado = !persona.Activo;
                    if (!_modoMemoriaLocal)
                    {
                        _servicioPersonas.CambiarEstadoPersona(id, nuevoEstado);
                    }
                    else
                    {
                        persona.Activo = nuevoEstado;
                    }
                    ActualizarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar el estado del registro: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
