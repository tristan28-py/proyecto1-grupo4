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

            // Aquí se conectará con la capa de negocio/datos
            MessageBox.Show($"¡Hola {username}! Las validaciones de interfaz funcionan correctamente.\nPronto conectaremos este formulario a SQL Server.", 
                "Prueba de Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
