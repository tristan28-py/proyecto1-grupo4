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

            lblUserInfo.Text = $"👤 Usuario: {_username} | Rol: {_role}";
            _currentButton = btnDashboard;
        }

        /// <summary>
        /// Embebe un formulario hijo dentro del panel de contenido principal.
        /// </summary>
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
                _currentButton.BackColor = Color.FromArgb(30, 58, 138); // Color original
                _currentButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            }

            _currentButton = clickedBtn;
            _currentButton.BackColor = Color.FromArgb(23, 44, 105); // Color seleccionado
            _currentButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }

        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            _activeForm?.Close();
            _activeForm = null;

            HighlightButton(btnDashboard);
            lblSectionTitle.Text = "Inicio / Dashboard";

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(pnlWelcome);
        }

        private void btnProducts_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnProducts);
            lblSectionTitle.Text = "Mantenimiento de Productos";
            // Próximo paso: OpenChildForm(new ProductsForm(), "Mantenimiento de Productos", btnProducts);
            ShowPlaceholder("Mantenimiento de Productos", "Aquí se gestionará el catálogo: altas, bajas, modificaciones, búsqueda con LINQ y stock mínimo.");
        }

        private void btnCategories_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnCategories);
            lblSectionTitle.Text = "Categorías";
            ShowPlaceholder("Mantenimiento de Categorías", "Gestión de familias de productos y estados activos/inactivos.");
        }

        private void btnClients_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnClients);
            lblSectionTitle.Text = "Clientes y Personas";
            ShowPlaceholder("Clientes y Empleados", "Administración de personas, clientes, crédito y empleados aplicando herencia de POO.");
        }

        private void btnSales_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnSales);
            lblSectionTitle.Text = "Registrar Venta";
            ShowPlaceholder("Módulo de Ventas", "Selección de cliente, detalle de venta con cálculo automático de totales y actualización atómica de inventario con transacción ADO.NET.");
        }

        private void btnOrders_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnOrders);
            lblSectionTitle.Text = "Pedidos Pendientes (Cola FIFO)";
            ShowPlaceholder("Cola de Pedidos", "Implementación de Queue<T> para atender pedidos pendientes en orden FIFO estricto.");
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnReports);
            lblSectionTitle.Text = "Reportes y Consultas LINQ";
            ShowPlaceholder("Reportes y Estadísticas", "Filtrado avanzado con LINQ sobre colecciones, productos con stock bajo y totales por cliente.");
        }

        private void btnHistory_Click(object? sender, EventArgs e)
        {
            HighlightButton(btnHistory);
            lblSectionTitle.Text = "Historial de Acciones (Pila LIFO)";
            ShowPlaceholder("Deshacer Acciones", "Implementación de Stack<T> para registrar acciones y permitir deshacer la última operación.");
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

        private void ShowPlaceholder(string section, string description)
        {
            pnlContent.Controls.Clear();

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            var title = new Label
            {
                Text = $"🛠️ {section}",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 138),
                AutoSize = true,
                Location = new Point(30, 30)
            };

            var desc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(75, 85, 99),
                Location = new Point(30, 80),
                Size = new Size(700, 80)
            };

            panel.Controls.Add(title);
            panel.Controls.Add(desc);
            pnlContent.Controls.Add(panel);
        }
    }
}
