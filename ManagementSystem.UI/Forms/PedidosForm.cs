using System;
using System.Windows.Forms;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class PedidosForm : Form
    {
        private readonly IServicioPedidos _servicioPedidos;
        private readonly IServicioProductos _servicioProductos;

        public PedidosForm()
        {
            InitializeComponent();
            _servicioPedidos = ServicioPedidos.InstanciaCompartida;
            _servicioProductos = new ServicioProductos();

            // Bloquear números en el nombre del cliente
            txtClientName.KeyPress += (s, e) =>
            {
                if (char.IsDigit(e.KeyChar)) e.Handled = true;
            };
        }

        private void PedidosForm_Load(object? sender, EventArgs e)
        {
            CargarProductos();
            ActualizarGrid();
        }

        private void CargarProductos()
        {
            cmbProduct.Items.Clear();
            try
            {
                var productos = _servicioProductos.ObtenerActivos();
                foreach (var producto in productos)
                {
                    cmbProduct.Items.Add(producto.Nombre);
                }
            }
            catch
            {
                // Manejo de error si no hay conexión
            }

            if (cmbProduct.Items.Count > 0)
            {
                cmbProduct.SelectedIndex = 0;
            }
        }

        private void ActualizarGrid()
        {
            dgvOrders.Rows.Clear();

            lblQueueCount.Text = $"Pedidos en cola: {_servicioPedidos.CantidadPendientes}";

            var siguiente = _servicioPedidos.VerSiguientePedido();
            if (siguiente != null)
            {
                lblPeekOrder.Text = $"Siguiente en turno: Pedido #{siguiente.PedidoId} - {siguiente.NombreCliente} ({siguiente.NombreProducto} x{siguiente.Cantidad})";
            }
            else
            {
                lblPeekOrder.Text = "Siguiente en turno: No hay pedidos pendientes";
            }

            int turno = 1;
            foreach (var pedido in _servicioPedidos.ObtenerPedidosPendientes())
            {
                dgvOrders.Rows.Add(
                    turno++,
                    $"#{pedido.PedidoId}",
                    pedido.NombreCliente,
                    pedido.NombreProducto,
                    pedido.Cantidad,
                    pedido.HoraRegistro.ToString("HH:mm:ss"),
                    pedido.Observaciones
                );
            }
        }

        private void btnEnqueue_Click(object? sender, EventArgs e)
        {
            try
            {
                string cliente = txtClientName.Text.Trim();
                if (string.IsNullOrWhiteSpace(cliente))
                {
                    MessageBox.Show("El nombre del cliente es obligatorio.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClientName.Focus();
                    return;
                }

                string producto = cmbProduct.SelectedItem?.ToString() ?? "Producto General";
                int cantidad = (int)nudQuantity.Value;

                var pedidoCreado = _servicioPedidos.EncolarPedido(cliente, producto, cantidad, txtNotes.Text.Trim());
                ServicioHistorial.InstanciaCompartida.RegistrarAccion(
                    modulo: "Pedidos",
                    descripcion: $"Se agregó a la cola el Pedido #{pedidoCreado.PedidoId} de '{cliente}'",
                    tipoAccion: "Registro",
                    entidadId: pedidoCreado.PedidoId);

                MessageBox.Show("Pedido agregado a la cola correctamente.", "Pedido Registrado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDequeue_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_servicioPedidos.CantidadPendientes == 0)
                {
                    MessageBox.Show("No hay pedidos pendientes en la cola para atender.", "Cola Vacía",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var atendido = _servicioPedidos.AtenderSiguientePedido();
                ServicioHistorial.InstanciaCompartida.RegistrarAccion(
                    modulo: "Pedidos",
                    descripcion: $"Se atendió el Pedido #{atendido.PedidoId} de '{atendido.NombreCliente}'",
                    tipoAccion: "Atención",
                    entidadId: atendido.PedidoId);

                MessageBox.Show(
                    $"Se atendió el Pedido #{atendido.PedidoId}:\n\nCliente: {atendido.NombreCliente}\nProducto: {atendido.NombreProducto} (x{atendido.Cantidad})",
                    "Pedido Atendido",
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
