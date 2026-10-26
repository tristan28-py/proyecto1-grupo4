using System;
using System.Windows.Forms;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class HistorialForm : Form
    {
        private readonly IServicioHistorial _servicioHistorial;

        public HistorialForm()
        {
            InitializeComponent();
            _servicioHistorial = ServicioHistorial.InstanciaCompartida;
        }

        private void HistorialForm_Load(object? sender, EventArgs e)
        {
            cmbModule.SelectedIndex = 0;
            ActualizarGrid();
        }

        private void ActualizarGrid()
        {
            dgvHistory.Rows.Clear();

            lblStackCount.Text = $"Acciones registradas: {_servicioHistorial.CantidadAcciones}";

            var cima = _servicioHistorial.VerUltimaAccion();
            if (cima != null)
            {
                lblPeekAction.Text = $"Última acción: [{cima.Modulo}] {cima.Descripcion}";
            }
            else
            {
                lblPeekAction.Text = "Última acción: El historial está vacío";
            }

            int posicion = 1;
            foreach (var accion in _servicioHistorial.ObtenerHistorial())
            {
                dgvHistory.Rows.Add(
                    posicion.ToString(),
                    accion.AccionId,
                    accion.Modulo,
                    accion.Descripcion,
                    accion.FechaHora.ToString("HH:mm:ss")
                );
                posicion++;
            }
        }

        private void btnPush_Click(object? sender, EventArgs e)
        {
            try
            {
                string descripcion = txtDescription.Text.Trim();
                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Ingrese la descripción de la acción.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescription.Focus();
                    return;
                }

                string modulo = cmbModule.SelectedItem?.ToString() ?? "General";
                _servicioHistorial.RegistrarAccion(modulo, descripcion);

                MessageBox.Show("Acción registrada en el historial.", "Acción Registrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtDescription.Clear();
                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPop_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_servicioHistorial.CantidadAcciones == 0)
                {
                    MessageBox.Show("No hay acciones en el historial para deshacer.", "Historial Vacío",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var deshecha = _servicioHistorial.DeshacerUltimaAccion();

                MessageBox.Show(
                    $"Se deshizo la última acción registrada:\n\nMódulo: {deshecha.Modulo}\nAcción: {deshecha.Descripcion}",
                    "Acción Deshecha",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            txtDescription.Clear();
            txtDescription.Focus();
        }
    }
}
