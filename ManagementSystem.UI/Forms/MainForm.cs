using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class MainForm : Form
    {
        private Form? _formularioActivo;
        private Button? _botonActual;
        private readonly string _nombreUsuario;
        private readonly string _rol;
        private readonly int _empleadoId;

        public MainForm(string nombreUsuario = "admin", string rol = "Administrador", int empleadoId = 1)
        {
            InitializeComponent();
            _nombreUsuario = nombreUsuario;
            _rol = rol;
            _empleadoId = empleadoId > 0 ? empleadoId : 1;

            lblInfoUsuario.Text = $"Usuario: {_nombreUsuario} | Rol: {_rol}";
            _botonActual = btnInicio;
        }

        public void AbrirFormularioHijo(Form formularioHijo, string titulo, Button botonOrigen)
        {
            _formularioActivo?.Close();

            ResaltarBoton(botonOrigen);

            _formularioActivo = formularioHijo;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(formularioHijo);
            pnlContenido.Tag = formularioHijo;

            lblTituloSeccion.Text = titulo;
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        private void ResaltarBoton(Button botonSeleccionado)
        {
            if (_botonActual != null)
            {
                _botonActual.BackColor = Color.FromArgb(30, 58, 138);
                _botonActual.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            }

            _botonActual = botonSeleccionado;
            _botonActual.BackColor = Color.FromArgb(23, 44, 105);
            _botonActual.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }

        private void btnInicio_Click(object? sender, EventArgs e)
        {
            _formularioActivo?.Close();
            _formularioActivo = null;

            ResaltarBoton(btnInicio);
            lblTituloSeccion.Text = "Inicio";

            pnlContenido.Controls.Clear();
            pnlContenido.Controls.Add(pnlBienvenida);
        }

        private void btnProductos_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new ProductosForm(), "Mantenimiento de Productos", btnProductos);
        }

        private void btnCategorias_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new CategoriasForm(), "Mantenimiento de Categorías", btnCategorias);
        }

        private void btnClientes_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new ClientesForm(), "Clientes y Personas", btnClientes);
        }

        private void btnVentas_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new VentasForm(_empleadoId), "Registrar Venta", btnVentas);
        }

        private void btnPedidos_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new PedidosForm(), "Pedidos Pendientes", btnPedidos);
        }

        private void btnReportes_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new ReportesForm(), "Reportes y Consultas", btnReportes);
        }

        private void btnHistorial_Click(object? sender, EventArgs e)
        {
            AbrirFormularioHijo(new HistorialForm(), "Historial de Acciones", btnHistorial);
        }

        private void btnCerrarSesion_Click(object? sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "¿Está seguro de que desea cerrar la sesión actual?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
