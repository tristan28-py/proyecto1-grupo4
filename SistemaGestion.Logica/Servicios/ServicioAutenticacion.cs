using System;
using System.Security.Cryptography;
using System.Text;
using ManagementSystem.DataAccess.ADONET;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio de negocio encargado de la autenticación de usuarios y cifrado SHA-256 de contraseñas.
    /// Compatible con el formato HASHBYTES('SHA2_256', ...) de SQL Server.
    /// </summary>
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ServicioAutenticacion()
            : this(new UsuarioRepositorioAdo())
        {
        }

        public ServicioAutenticacion(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        /// <summary>
        /// Genera el hash SHA-256 en formato hexadecimal en minúsculas (64 caracteres).
        /// </summary>
        public string GenerarHashSha256(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano))
            {
                return string.Empty;
            }

            byte[] bytesEntrada = Encoding.ASCII.GetBytes(textoPlano);
            byte[] bytesHash = SHA256.HashData(bytesEntrada);

            var constructorHex = new StringBuilder(bytesHash.Length * 2);
            foreach (byte b in bytesHash)
            {
                constructorHex.Append(b.ToString("x2"));
            }

            return constructorHex.ToString();
        }

        /// <summary>
        /// Valida las credenciales de inicio de sesión contra la base de datos SQL Server.
        /// </summary>
        public Usuario? IniciarSesion(string nombreUsuario, string contrasenaPlana, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                mensajeError = "Por favor, ingrese su nombre de usuario.";
                return null;
            }

            if (string.IsNullOrWhiteSpace(contrasenaPlana))
            {
                mensajeError = "Por favor, ingrese su contraseña.";
                return null;
            }

            string contrasenaHash = GenerarHashSha256(contrasenaPlana.Trim());
            Usuario? usuario = _usuarioRepositorio.ValidarCredenciales(nombreUsuario.Trim(), contrasenaHash);

            if (usuario == null)
            {
                mensajeError = "Usuario o contraseña incorrectos, o la cuenta se encuentra inactiva.";
                return null;
            }

            return usuario;
        }
    }
}
