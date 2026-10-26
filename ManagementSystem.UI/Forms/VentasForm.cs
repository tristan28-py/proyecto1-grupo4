using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaGestion.Entidades.Entidades;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class VentasForm : Form
    {
        private class ClienteComboItem
        {
            public int ClienteId { get; set; }
            public string TextoMostrar { get; set; } = string.Empty;
            public override string ToString() => TextoMostrar;
        }

        private readonly IServicioVentas _servicioVentas;
        private readonly IServicioProductos _servicioProductos;
        private readonly IServicioPersonas _servicioPersonas;
        private readonly int _empleadoId;

        private List<Producto> _inventario = new();
        private readonly List<DetalleVenta> _detalles = new();
        private bool _modoMemoriaLocal;

        public VentasForm(int empleadoId = 1)
        {
            InitializeComponent();
            _empleadoId = empleadoId > 0 ? empleadoId : 1;
            _servicioVentas = new ServicioVentas();
            _servicioProductos = new ServicioProductos();
            _servicioPersonas = new ServicioPersonas();
        }

        private void VentasForm_Load(object? sender, EventArgs e)
        {
            CargarCatalogos();
            ActualizarGrid();
        }

        private void CargarCatalogos()
        {
            cmbClient.Items.Clear();
            try
            {
                var clientes = _servicioPersonas.ObtenerClientesActivos();
                foreach (var cliente in clientes)
                {
                    cmbClient.Items.Add(new ClienteComboItem
                    {
                        ClienteId = cliente.ClienteId,
                        TextoMostrar = $"{cliente.NombreCompleto} ({cliente.Identificacion})"
                    });
                }

                _inventario = _servicioProductos.ObtenerActivos();
                _modoMemoriaLocal = false;
            }
            catch
            {
                _modoMemoriaLocal = true;
            }

            if (cmbClient.Items.Count == 0)
            {
                cmbClient.Items.AddRange(new object[]
                {
                    new ClienteComboItem { ClienteId = 1, TextoMostrar = "Juan Pérez Gómez (1-0987-0654)" },
                    new ClienteComboItem { ClienteId = 2, TextoMostrar = "Empresa Alfa S.A. (3-101-998877)" }
                });
            }
            cmbClient.SelectedIndex = 0;

            cmbProduct.Items.Clear();
            foreach (var producto in _inventario)
            {
                cmbProduct.Items.Add(producto);
            }

            if (cmbProduct.Items.Count > 0)
            {
                cmbProduct.SelectedIndex = 0;
            }
            else
            {
                nudUnitPrice.Value = 0;
                lblStockInfo.Text = "No hay productos registrados";
                lblLineSubTotal.Text = "Subtotal línea: ₡0,00";
            }
        }

        private void cmbProduct_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem is not Producto producto) return;

            nudUnitPrice.Value = producto.PrecioVenta;
            lblStockInfo.Text = $"Stock disponible: {producto.StockActual}";
            ActualizarPreviewSubTotal();
        }

        private void nudQuantity_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarPreviewSubTotal();
        }

        private void ActualizarPreviewSubTotal()
        {
            decimal subTotal = nudQuantity.Value * nudUnitPrice.Value;
            lblLineSubTotal.Text = $"Subtotal línea: {subTotal:C2}";
        }

        private void btnAddDetail_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.SelectedItem is not Producto producto) return;

                int cantidad = (int)nudQuantity.Value;
                var existente = _detalles.FirstOrDefault(d => d.ProductoId == producto.ProductoId);
                int totalSolicitado = (existente?.Cantidad ?? 0) + cantidad;

                _servicioVentas.ValidarStockDisponible(producto, totalSolicitado);
                decimal subTotalCalculado = _servicioVentas.CalcularSubTotalLinea(totalSolicitado, producto.PrecioVenta);

                if (existente != null)
                {
                    existente.Cantidad = totalSolicitado;
                    existente.SubTotal = subTotalCalculado;
                }
                else
                {
                    _detalles.Add(new DetalleVenta
                    {
                        ProductoId = producto.ProductoId,
                        Producto = producto,
                        Cantidad = cantidad,
                        PrecioUnitario = producto.PrecioVenta,
                        SubTotal = _servicioVentas.CalcularSubTotalLinea(cantidad, producto.PrecioVenta)
                    });
                }

                nudQuantity.Value = 1;
                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stock Insuficiente / Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemoveDetail_Click(object? sender, EventArgs e)
        {
            if (dgvSaleDetails.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto del detalle para quitarlo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productoId = Convert.ToInt32(dgvSaleDetails.SelectedRows[0].Cells["colId"].Value);
            var item = _detalles.Find(d => d.ProductoId == productoId);
            if (item != null)
            {
                _detalles.Remove(item);
                ActualizarGrid();
            }
        }

        private void ActualizarGrid()
        {
            dgvSaleDetails.Rows.Clear();

            foreach (var detalle in _detalles)
            {
                string nombreProducto = detalle.Producto?.Nombre ?? $"Producto #{detalle.ProductoId}";
                dgvSaleDetails.Rows.Add(
                    detalle.ProductoId,
                    nombreProducto,
                    detalle.Cantidad,
                    detalle.PrecioUnitario.ToString("C2"),
                    detalle.SubTotal.ToString("C2")
                );
            }

            decimal totalVenta = _detalles.Count > 0 ? _servicioVentas.CalcularTotalVenta(_detalles) : 0m;
            lblTotalSale.Text = $"Total Venta: {totalVenta:C2}";
        }

        private void btnConfirmSale_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_detalles.Count == 0)
                {
                    MessageBox.Show("Una venta debe tener al menos un detalle.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var clienteSeleccionado = cmbClient.SelectedItem as ClienteComboItem;
                int clienteId = clienteSeleccionado?.ClienteId ?? 1;
                decimal totalVenta = _servicioVentas.CalcularTotalVenta(_detalles);

                var nuevaVenta = new Venta
                {
                    ClienteId = clienteId,
                    EmpleadoId = _empleadoId,
                    FechaVenta = dtpSaleDate.Value,
                    SubTotal = totalVenta,
                    Impuesto = 0m,
                    Descuento = 0m,
                    Total = totalVenta,
                    Estado = Venta.EstadoProcesada,
                    Observaciones = "Venta confirmada desde módulo de facturación"
                };

                if (!_modoMemoriaLocal)
                {
                    int ventaId = _servicioVentas.RegistrarVenta(nuevaVenta, _detalles, _empleadoId, confirmarVenta: true);
                    CargarCatalogos();
                    MessageBox.Show(
                        $"Venta #{ventaId} registrada correctamente para '{clienteSeleccionado?.TextoMostrar}'.\nLas existencias de inventario fueron actualizadas.\nTotal facturado: {totalVenta:C2}",
                        "Venta Confirmada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    foreach (var detalle in _detalles)
                    {
                        var producto = _inventario.FirstOrDefault(p => p.ProductoId == detalle.ProductoId);
                        if (producto != null)
                        {
                            producto.StockActual -= detalle.Cantidad;
                        }
                    }

                    MessageBox.Show(
                        $"Venta registrada correctamente para '{clienteSeleccionado?.TextoMostrar}'.\nTotal facturado: {totalVenta:C2}",
                        "Venta Confirmada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                _detalles.Clear();
                cmbProduct_SelectedIndexChanged(null, EventArgs.Empty);
                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al confirmar la venta: {ex.Message}", "Error de Transacción",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewSale_Click(object? sender, EventArgs e)
        {
            // Se descartan las líneas temporales sin modificar el inventario
            _detalles.Clear();
            nudQuantity.Value = 1;
            ActualizarGrid();
        }
    }
}
