using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class MainForm : Form
    {
        private Form? _activeForm;
        private Button? _currentButton;
        private readonly string _username;
        private readonly string _role;

        public MainForm(string username = "admin", string role = "Administrador")
        {
            InitializeComponent();
            _username = username;
            _role = role;

            lblUserInfo.Text = $"Usuario: {_username} | Rol: {_role}";
            _currentButton = btnDashboard;
        }

        public void OpenChildForm(Form childForm, string title, Button senderBtn)
        {
            _activeForm?.Close();

            HighlightButton(senderBtn);

            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;

            lblSectionTitle.Text = title;
            childForm.BringToFront();
            childForm.Show();
        }

        private void HighlightButton(Button clickedBtn)
        {
            if (_currentButton != null)
            {
                _currentButton.BackColor = Color.FromArgb(30, 58, 138);
                _currentButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            }

            _currentButton = clickedBtn;
            _currentButton.BackColor = Color.FromArgb(23, 44, 105);
            _currentButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }

        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            _activeForm?.Close();
            _activeForm = null;

            HighlightButton(btnDashboard);
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
            OpenChildForm(new VentasForm(), "Registrar Venta", btnSales);
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
            var confirm = MessageBox.Show(
                "¿Está seguro de que desea cerrar la sesión actual?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
