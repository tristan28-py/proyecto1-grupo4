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

        public MainForm(string username = "admin", string role = "Administrador", int empleadoId = 1)
        {
            InitializeComponent();
            _nombreUsuario = username;
            _rol = role;
            _empleadoId = empleadoId > 0 ? empleadoId : 1;

            lblUserInfo.Text = $"Usuario: {_nombreUsuario} | Rol: {_rol}";
            _botonActual = btnDashboard;
        }

        public void OpenChildForm(Form formularioHijo, string titulo, Button botonOrigen)
        {
            _formularioActivo?.Close();

            ResaltarBoton(botonOrigen);

            _formularioActivo = formularioHijo;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(formularioHijo);
            pnlContent.Tag = formularioHijo;

            lblSectionTitle.Text = titulo;
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

        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            _formularioActivo?.Close();
            _formularioActivo = null;

            ResaltarBoton(btnDashboard);
            lblSectionTitle.Text = "Inicio";

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(pnlWelcome);
        }

        private void btnProducts_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new ProductosForm(), "Mantenimiento de Productos", btnProducts);
        }

        private void btnCategories_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new CategoriasForm(), "Mantenimiento de Categorías", btnCategories);
        }

        private void btnClients_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new ClientesForm(), "Clientes y Personas", btnClients);
        }

        private void btnSales_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new VentasForm(_empleadoId), "Registrar Venta", btnSales);
        }

        private void btnOrders_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new PedidosForm(), "Pedidos Pendientes", btnOrders);
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new ReportesForm(), "Reportes y Consultas", btnReports);
        }

        private void btnHistory_Click(object? sender, EventArgs e)
        {
            OpenChildForm(new HistorialForm(), "Historial de Acciones", btnHistory);
        }

        private void btnLogout_Click(object? sender, EventArgs e)
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
