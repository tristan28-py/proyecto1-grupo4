using System;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class LoginForms : Form
    {
        public LoginForms()
        {
            InitializeComponent(); 

        }

        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            // Muestra u oculta la contraseña
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validación básica de campos vacíos
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Por favor, ingrese su nombre de usuario.", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, ingrese su contraseña.", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Asignación de rol por usuario (temporal hasta conectar con AuthService / SQL Server)
            string rol = username.ToLower() switch
            {
                "admin" => "Administrador",
                "vendedor" => "Vendedor",
                "bodega" => "Bodeguero",
                _ => "Administrador"
            };

            // Abrir el formulario principal
            var mainForm = new MainForm(username, rol);
            this.Hide();

            mainForm.FormClosed += (s, args) =>
            {
                txtPassword.Clear();
                this.Show();
                txtUsername.Focus();
            };

            mainForm.Show();
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object? sender, EventArgs e)
        {

        }
    }
}
