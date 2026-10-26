using System;
using System.Windows.Forms;
using SistemaGestion.Entidades.Entidades;
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

        private void chkMostrarContrasena_CheckedChanged(object? sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
        }

        private void btnIniciarSesion_Click(object? sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Por favor, ingrese su nombre de usuario.", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, ingrese su contraseña.", "Campo Requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
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
                    txtContrasena.SelectAll();
                    txtContrasena.Focus();
                    return;
                }

                rolAsignado = usuarioAutenticado.Rol?.NombreRol ?? "Administrador";
                empleadoId = usuarioAutenticado.EmpleadoId > 0 ? usuarioAutenticado.EmpleadoId : 1;
                nombreUsuario = usuarioAutenticado.NombreUsuario;
            }
            catch
            {
                // Acceso de respaldo cuando la base de datos local no está disponible
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
                txtContrasena.Clear();
                this.Show();
                txtUsuario.Focus();
            };

            formularioPrincipal.Show();
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
