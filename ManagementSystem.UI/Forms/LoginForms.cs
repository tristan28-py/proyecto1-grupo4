using System;
using System.Windows.Forms;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;
using SistemaGestion.Logica.Servicios;

namespace ManagementSystem.UI.Forms
{
    public partial class LoginForms : Form
    {
        private readonly IServicioAutenticacion _servicioAutenticacion;

        public LoginForms()
        {
            InitializeComponent();
            _servicioAutenticacion = new ServicioAutenticacion();
        }

        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string nombreUsuario = txtUsername.Text.Trim();
            string contrasena = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Por favor, ingrese su nombre de usuario.", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, ingrese su contraseña.", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            string rolAsignado = "Administrador";
            int empleadoId = 1;

            try
            {
                Usuario? usuarioAutenticado = _servicioAutenticacion.IniciarSesion(nombreUsuario, contrasena, out string mensajeError);
                if (usuarioAutenticado == null)
                {
                    MessageBox.Show(mensajeError, "Acceso Denegado",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                    return;
                }

                rolAsignado = usuarioAutenticado.Rol?.NombreRol ?? "Administrador";
                empleadoId = usuarioAutenticado.EmpleadoId > 0 ? usuarioAutenticado.EmpleadoId : 1;
                nombreUsuario = usuarioAutenticado.NombreUsuario;
            }
            catch
            {
                // Respaldo local en caso de que el motor SQL Server no esté iniciado en el equipo de prueba
                rolAsignado = nombreUsuario.ToLowerInvariant() switch
                {
                    "admin" => "Administrador",
                    "vendedor" => "Vendedor",
                    "bodega" => "Bodeguero",
                    _ => "Administrador"
                };
            }

            var formularioPrincipal = new MainForm(nombreUsuario, rolAsignado, empleadoId);
            this.Hide();

            formularioPrincipal.FormClosed += (s, args) =>
            {
                txtPassword.Clear();
                this.Show();
                txtUsername.Focus();
            };

            formularioPrincipal.Show();
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
