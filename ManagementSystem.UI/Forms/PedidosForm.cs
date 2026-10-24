using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class PedidosForm : Form
    {
        private class PedidoViewModel
        {
            public int Id { get; set; }
            public string Cliente { get; set; } = string.Empty;
            public string Producto { get; set; } = string.Empty;
            public int Cantidad { get; set; }
            public DateTime HoraRegistro { get; set; } = DateTime.Now;
            public string Observaciones { get; set; } = string.Empty;
        }

        private static readonly Queue<PedidoViewModel> _colaPedidos = new();
        private static int _consecutivo = 101;
        private static bool _datosInicialesCargados;

        public PedidosForm()
        {
            InitializeComponent();
        }

        private void PedidosForm_Load(object? sender, EventArgs e)
        {
            CargarProductos();
            if (!_datosInicialesCargados)
            {
                CargarDatosDePrueba();
                _datosInicialesCargados = true;
            }
            ActualizarGrid();
        }

        private void CargarProductos()
        {
            cmbProduct.Items.Clear();
            cmbProduct.Items.AddRange(new object[]
            {
                "Laptop Dell Inspiron 15",
                "Mouse Inalámbrico Logitech M185",
                "Teclado Mecánico RGB",
                "Monitor Samsung 24 pulg FHD",
                "Resma Papel Bond A4 75g"
            });
            if (cmbProduct.Items.Count > 0)
                cmbProduct.SelectedIndex = 0;
        }

        private void CargarDatosDePrueba()
        {
            _colaPedidos.Enqueue(new PedidoViewModel
            {
                Id = _consecutivo++,
                Cliente = "María Fernández López",
                Producto = "Laptop Dell Inspiron 15",
                Cantidad = 2,
                HoraRegistro = DateTime.Now.AddMinutes(-20),
                Observaciones = "Entrega en sucursal central"
            });

            _colaPedidos.Enqueue(new PedidoViewModel
            {
                Id = _consecutivo++,
                Cliente = "Comercial El Sol S.A.",
                Producto = "Monitor Samsung 24 pulg FHD",
                Cantidad = 5,
                HoraRegistro = DateTime.Now.AddMinutes(-10),
                Observaciones = "Facturar a crédito"
            });
        }

        private void ActualizarGrid()
        {
            dgvOrders.Rows.Clear();

            lblQueueCount.Text = $"Pedidos en cola: {_colaPedidos.Count}";

            if (_colaPedidos.Count > 0)
            {
                var siguiente = _colaPedidos.Peek();
                lblPeekOrder.Text = $"Siguiente en turno: Pedido #{siguiente.Id} - {siguiente.Cliente} ({siguiente.Producto} x{siguiente.Cantidad})";
            }
            else
            {
                lblPeekOrder.Text = "Siguiente en turno: No hay pedidos pendientes";
            }

            int turno = 1;
            foreach (var p in _colaPedidos)
            {
                dgvOrders.Rows.Add(
                    turno++,
                    $"#{p.Id}",
                    p.Cliente,
                    p.Producto,
                    p.Cantidad,
                    p.HoraRegistro.ToString("HH:mm:ss"),
                    p.Observaciones
                );
            }
        }

        private void btnEnqueue_Click(object? sender, EventArgs e)
        {
            string cliente = txtClientName.Text.Trim();
            if (string.IsNullOrWhiteSpace(cliente))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClientName.Focus();
                return;
            }

            _colaPedidos.Enqueue(new PedidoViewModel
            {
                Id = _consecutivo++,
                Cliente = cliente,
                Producto = cmbProduct.SelectedItem?.ToString() ?? "Producto General",
                Cantidad = (int)nudQuantity.Value,
                HoraRegistro = DateTime.Now,
                Observaciones = txtNotes.Text.Trim()
            });

            MessageBox.Show("Pedido agregado a la cola correctamente.", "Pedido Registrado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            ActualizarGrid();
        }

        private void btnDequeue_Click(object? sender, EventArgs e)
        {
            if (_colaPedidos.Count == 0)
            {
                MessageBox.Show("No hay pedidos pendientes en la cola para atender.", "Cola Vacía",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var atendido = _colaPedidos.Dequeue();

            MessageBox.Show(
                $"Se atendió el Pedido #{atendido.Id}:\n\nCliente: {atendido.Cliente}\nProducto: {atendido.Producto} (x{atendido.Cantidad})",
                "Pedido Atendido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ActualizarGrid();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtClientName.Clear();
            if (cmbProduct.Items.Count > 0) cmbProduct.SelectedIndex = 0;
            nudQuantity.Value = 1;
            txtNotes.Clear();
            txtClientName.Focus();
        }
    }
}
