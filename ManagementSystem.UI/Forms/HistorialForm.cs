using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class HistorialForm : Form
    {
        private class AccionViewModel
        {
            public int Id { get; set; }
            public string Modulo { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
            public DateTime Hora { get; set; } = DateTime.Now;
        }

        private static readonly Stack<AccionViewModel> _pilaHistorial = new();
        private static int _consecutivo = 1;
        private static bool _datosInicialesCargados;

        public HistorialForm()
        {
            InitializeComponent();
        }

        private void HistorialForm_Load(object? sender, EventArgs e)
        {
            cmbModule.SelectedIndex = 0;
            if (!_datosInicialesCargados)
            {
                CargarDatosDePrueba();
                _datosInicialesCargados = true;
            }
            ActualizarGrid();
        }

        private void CargarDatosDePrueba()
        {
            _pilaHistorial.Push(new AccionViewModel
            {
                Id = _consecutivo++,
                Modulo = "Categorías",
                Descripcion = "Se registró la categoría 'Electrónica y Cómputo'",
                Hora = DateTime.Now.AddMinutes(-25)
            });

            _pilaHistorial.Push(new AccionViewModel
            {
                Id = _consecutivo++,
                Modulo = "Productos",
                Descripcion = "Se actualizó el stock de 'Laptop Dell Inspiron 15'",
                Hora = DateTime.Now.AddMinutes(-12)
            });

            _pilaHistorial.Push(new AccionViewModel
            {
                Id = _consecutivo++,
                Modulo = "Ventas",
                Descripcion = "Se registró la venta #101 para María Fernández",
                Hora = DateTime.Now.AddMinutes(-4)
            });
        }

        private void ActualizarGrid()
        {
            dgvHistory.Rows.Clear();

            lblStackCount.Text = $"Acciones registradas: {_pilaHistorial.Count}";

            if (_pilaHistorial.Count > 0)
            {
                var cima = _pilaHistorial.Peek();
                lblPeekAction.Text = $"Última acción: [{cima.Modulo}] {cima.Descripcion}";
            }
            else
            {
                lblPeekAction.Text = "Última acción: El historial está vacío";
            }

            int pos = 1;
            foreach (var a in _pilaHistorial)
            {
                dgvHistory.Rows.Add(
                    pos.ToString(),
                    a.Id,
                    a.Modulo,
                    a.Descripcion,
                    a.Hora.ToString("HH:mm:ss")
                );
                pos++;
            }
        }

        private void btnPush_Click(object? sender, EventArgs e)
        {
            string desc = txtDescription.Text.Trim();
            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Ingrese la descripción de la acción.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return;
            }

            _pilaHistorial.Push(new AccionViewModel
            {
                Id = _consecutivo++,
                Modulo = cmbModule.SelectedItem?.ToString() ?? "General",
                Descripcion = desc,
                Hora = DateTime.Now
            });

            MessageBox.Show("Acción registrada en el historial.", "Acción Registrada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtDescription.Clear();
            ActualizarGrid();
        }

        private void btnPop_Click(object? sender, EventArgs e)
        {
            if (_pilaHistorial.Count == 0)
            {
                MessageBox.Show("No hay acciones en el historial para deshacer.", "Historial Vacío",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var deshecha = _pilaHistorial.Pop();

            MessageBox.Show(
                $"Se deshizo la última acción:\n\nMódulo: {deshecha.Modulo}\nAcción: {deshecha.Descripcion}",
                "Acción Deshecha",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ActualizarGrid();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            txtDescription.Clear();
            txtDescription.Focus();
        }
    }
}
