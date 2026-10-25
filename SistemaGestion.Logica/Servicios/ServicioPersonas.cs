using System;
using System.Collections.Generic;
using System.Linq;
using ManagementSystem.DataAccess.ADONET;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.DTOs;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio de negocio para la administración de Personas, Clientes, Empleados y Usuarios.
    /// Demuestra el uso explícito de Herencia y Polimorfismo sobre la jerarquía Persona -> Cliente / Empleado -> Usuario.
    /// </summary>
    public class ServicioPersonas : IServicioPersonas
    {
        private readonly IPersonaRepositorio _personaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IEmpleadoRepositorio _empleadoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IServicioAutenticacion _servicioAutenticacion;
        private readonly IServicioHistorial _servicioHistorial;

        public ServicioPersonas()
            : this(
                new PersonaRepositorioAdo(),
                new ClienteRepositorioAdo(),
                new EmpleadoRepositorioAdo(),
                new UsuarioRepositorioAdo(),
                new RolRepositorioAdo(),
                new ServicioAutenticacion(),
                ServicioHistorial.InstanciaCompartida)
        {
        }

        public ServicioPersonas(
            IPersonaRepositorio personaRepositorio,
            IClienteRepositorio clienteRepositorio,
            IEmpleadoRepositorio empleadoRepositorio,
            IUsuarioRepositorio usuarioRepositorio,
            IRolRepositorio rolRepositorio,
            IServicioAutenticacion? servicioAutenticacion = null,
            IServicioHistorial? servicioHistorial = null)
        {
            _personaRepositorio = personaRepositorio ?? throw new ArgumentNullException(nameof(personaRepositorio));
            _clienteRepositorio = clienteRepositorio ?? throw new ArgumentNullException(nameof(clienteRepositorio));
            _empleadoRepositorio = empleadoRepositorio ?? throw new ArgumentNullException(nameof(empleadoRepositorio));
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
            _rolRepositorio = rolRepositorio ?? throw new ArgumentNullException(nameof(rolRepositorio));
            _servicioAutenticacion = servicioAutenticacion ?? new ServicioAutenticacion();
            _servicioHistorial = servicioHistorial ?? ServicioHistorial.InstanciaCompartida;
        }

        public List<Cliente> ObtenerClientes()
        {
            return _clienteRepositorio.ObtenerTodos().OrderBy(c => c.NombreCompleto).ToList();
        }

        public List<Cliente> ObtenerClientesActivos()
        {
            return _clienteRepositorio.ObtenerTodos()
                .Where(c => c.Activo)
                .OrderBy(c => c.NombreCompleto)
                .ToList();
        }

        public List<Empleado> ObtenerEmpleados()
        {
            return _empleadoRepositorio.ObtenerTodos().OrderBy(e => e.NombreCompleto).ToList();
        }

        public List<Rol> ObtenerRoles()
        {
            return _rolRepositorio.ObtenerRolesActivos().ToList();
        }

        public List<ResumenPersonaDto> ObtenerResumenPersonas()
        {
            return _personaRepositorio.ObtenerResumenPersonas().OrderBy(p => p.NombreCompleto).ToList();
        }

        public List<ResumenPersonaDto> FiltrarResumenPersonas(string? textoBusqueda, string? tipoFiltro)
        {
            List<ResumenPersonaDto> todos = ObtenerResumenPersonas();
            IEnumerable<ResumenPersonaDto> consulta = todos;

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                consulta = consulta.Where(p =>
                    p.NombreCompleto.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    p.Identificacion.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    (p.Email != null && p.Email.Contains(filtro, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrWhiteSpace(tipoFiltro) && !tipoFiltro.Equals("Todos", StringComparison.OrdinalIgnoreCase))
            {
                if (tipoFiltro.StartsWith("Cliente", StringComparison.OrdinalIgnoreCase))
                {
                    consulta = consulta.Where(p => p.EsCliente);
                }
                else if (tipoFiltro.StartsWith("Empleado", StringComparison.OrdinalIgnoreCase))
                {
                    consulta = consulta.Where(p => p.EsEmpleado);
                }
                else if (tipoFiltro.StartsWith("Usuario", StringComparison.OrdinalIgnoreCase))
                {
                    consulta = consulta.Where(p => p.TieneUsuario);
                }
            }

            return consulta.OrderBy(p => p.NombreCompleto).ToList();
        }

        /// <summary>
        /// Demuestra Polimorfismo en tiempo de ejecución tratando instancias de Cliente, Empleado y Usuario
        /// a través de una lista de su clase base Persona e invocando el método virtual ObtenerResumenPerfil().
        /// </summary>
        public List<string> ObtenerPerfilesPolimorficos()
        {
            var personasPolimorficas = new List<Persona>();
            personasPolimorficas.AddRange(_clienteRepositorio.ObtenerTodos());
            personasPolimorficas.AddRange(_empleadoRepositorio.ObtenerTodos());

            var resumenes = new List<string>();
            foreach (Persona persona in personasPolimorficas)
            {
                resumenes.Add(persona.ObtenerResumenPerfil());
            }

            return resumenes;
        }

        public bool ValidarDatosPersona(
            string nombreCompleto,
            string identificacion,
            decimal montoAsignado,
            out List<string> errores)
        {
            errores = new List<string>();

            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                errores.Add("El nombre completo es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(identificacion))
            {
                errores.Add("La identificación es obligatoria.");
            }

            if (montoAsignado < 0)
            {
                errores.Add("El monto asignado (límite de crédito o salario) no puede ser negativo.");
            }

            return errores.Count == 0;
        }

        /// <summary>
        /// Crea o actualiza una Persona y su perfil asociado (Cliente, Empleado o Usuario).
        /// </summary>
        public int GuardarPersonaSegunTipo(ResumenPersonaDto datos, string? contrasenaPlana = null)
        {
            if (datos == null)
            {
                throw new ArgumentNullException(nameof(datos));
            }

            decimal monto = datos.TiposPersona.Equals("Cliente", StringComparison.OrdinalIgnoreCase)
                ? (datos.LimiteCredito ?? 0m)
                : (datos.Salario ?? 0m);

            if (!ValidarDatosPersona(datos.NombreCompleto, datos.Identificacion, monto, out List<string> errores))
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errores));
            }

            int personaId = datos.PersonaId;
            var entidadPersona = new Persona
            {
                PersonaId = personaId,
                NombreCompleto = datos.NombreCompleto.Trim(),
                Identificacion = datos.Identificacion.Trim(),
                Email = datos.Email?.Trim(),
                Telefono = datos.Telefono?.Trim(),
                Direccion = datos.Direccion?.Trim(),
                Activo = datos.Activo
            };

            if (personaId <= 0)
            {
                personaId = _personaRepositorio.Insertar(entidadPersona);
                datos.PersonaId = personaId;
            }
            else
            {
                _personaRepositorio.Actualizar(entidadPersona);
            }

            string tipo = string.IsNullOrWhiteSpace(datos.TiposPersona) ? "Cliente" : datos.TiposPersona.Trim();

            if (tipo.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
            {
                var todosClientes = _clienteRepositorio.ObtenerTodos().ToList();
                Cliente? clienteExistente = todosClientes.FirstOrDefault(c => c.PersonaId == personaId);

                if (clienteExistente == null)
                {
                    var nuevoCliente = new Cliente
                    {
                        PersonaId = personaId,
                        NombreCompleto = entidadPersona.NombreCompleto,
                        Identificacion = entidadPersona.Identificacion,
                        Email = entidadPersona.Email,
                        Telefono = entidadPersona.Telefono,
                        Direccion = entidadPersona.Direccion,
                        LimiteCredito = datos.LimiteCredito ?? 0m,
                        Activo = datos.Activo
                    };
                    _clienteRepositorio.Insertar(nuevoCliente);
                }
                else
                {
                    clienteExistente.NombreCompleto = entidadPersona.NombreCompleto;
                    clienteExistente.Identificacion = entidadPersona.Identificacion;
                    clienteExistente.Email = entidadPersona.Email;
                    clienteExistente.Telefono = entidadPersona.Telefono;
                    clienteExistente.Direccion = entidadPersona.Direccion;
                    clienteExistente.LimiteCredito = datos.LimiteCredito ?? 0m;
                    clienteExistente.Activo = datos.Activo;
                    _clienteRepositorio.Actualizar(clienteExistente);
                }
            }
            else if (tipo.Equals("Empleado", StringComparison.OrdinalIgnoreCase) ||
                     tipo.Equals("Usuario", StringComparison.OrdinalIgnoreCase))
            {
                Empleado? empleadoExistente = _empleadoRepositorio.ObtenerPorPersonaId(personaId);
                int empleadoId;

                if (empleadoExistente == null)
                {
                    var nuevoEmpleado = new Empleado
                    {
                        PersonaId = personaId,
                        NombreCompleto = entidadPersona.NombreCompleto,
                        Identificacion = entidadPersona.Identificacion,
                        Email = entidadPersona.Email,
                        Telefono = entidadPersona.Telefono,
                        Direccion = entidadPersona.Direccion,
                        Puesto = string.IsNullOrWhiteSpace(datos.Puesto) ? "Ejecutivo de Gestión" : datos.Puesto.Trim(),
                        Salario = datos.Salario ?? (datos.LimiteCredito ?? 0m),
                        Activo = datos.Activo
                    };
                    empleadoId = _empleadoRepositorio.Insertar(nuevoEmpleado);
                }
                else
                {
                    empleadoExistente.Puesto = string.IsNullOrWhiteSpace(datos.Puesto) ? empleadoExistente.Puesto : datos.Puesto.Trim();
                    empleadoExistente.Salario = datos.Salario ?? (datos.LimiteCredito ?? empleadoExistente.Salario);
                    empleadoExistente.Activo = datos.Activo;
                    _empleadoRepositorio.Actualizar(empleadoExistente);
                    empleadoId = empleadoExistente.EmpleadoId;
                }

                if (tipo.Equals("Usuario", StringComparison.OrdinalIgnoreCase))
                {
                    Usuario? usuarioExistente = _usuarioRepositorio.ObtenerPorEmpleadoId(empleadoId);
                    string nombreUsuarioGenerado = !string.IsNullOrWhiteSpace(datos.NombreUsuario)
                        ? datos.NombreUsuario.Trim()
                        : GenerarNombreUsuarioBase(entidadPersona.NombreCompleto, personaId);

                    int rolId = datos.RolId.HasValue && datos.RolId.Value > 0 ? datos.RolId.Value : 2;
                    string hashContrasena = !string.IsNullOrWhiteSpace(contrasenaPlana)
                        ? _servicioAutenticacion.GenerarHashSha256(contrasenaPlana)
                        : _servicioAutenticacion.GenerarHashSha256("usuario123");

                    if (usuarioExistente == null)
                    {
                        _usuarioRepositorio.Insertar(new Usuario
                        {
                            EmpleadoId = empleadoId,
                            NombreUsuario = nombreUsuarioGenerado,
                            ContrasenaHash = hashContrasena,
                            RolId = rolId,
                            Activo = datos.Activo
                        });
                    }
                    else
                    {
                        usuarioExistente.NombreUsuario = nombreUsuarioGenerado;
                        usuarioExistente.RolId = rolId;
                        usuarioExistente.Activo = datos.Activo;
                        if (!string.IsNullOrWhiteSpace(contrasenaPlana))
                        {
                            usuarioExistente.ContrasenaHash = hashContrasena;
                        }
                        _usuarioRepositorio.Actualizar(usuarioExistente);
                    }
                }
            }

            _servicioHistorial.RegistrarAccion(
                modulo: "Clientes",
                descripcion: $"Se guardó el registro de '{entidadPersona.NombreCompleto}' ({tipo})",
                tipoAccion: "Mantenimiento",
                entidadId: personaId);

            return personaId;
        }

        public bool CambiarEstadoPersona(int personaId, bool nuevoEstado)
        {
            Persona? existente = _personaRepositorio.ObtenerPorId(personaId);
            if (existente == null)
            {
                return false;
            }

            bool estadoAnterior = existente.Activo;
            existente.Activo = nuevoEstado;
            bool actualizado = _personaRepositorio.Actualizar(existente);

            var cliente = _clienteRepositorio.ObtenerTodos().FirstOrDefault(c => c.PersonaId == personaId);
            if (cliente != null)
            {
                cliente.Activo = nuevoEstado;
                _clienteRepositorio.Actualizar(cliente);
            }

            var empleado = _empleadoRepositorio.ObtenerPorPersonaId(personaId);
            if (empleado != null)
            {
                empleado.Activo = nuevoEstado;
                _empleadoRepositorio.Actualizar(empleado);
            }

            if (actualizado)
            {
                string estadoTexto = nuevoEstado ? "Activo" : "Inactivo";
                _servicioHistorial.RegistrarAccion(
                    modulo: "Clientes",
                    descripcion: $"Se cambió el estado de '{existente.NombreCompleto}' a {estadoTexto}",
                    tipoAccion: "Estado",
                    entidadId: personaId,
                    accionDeshacer: () => CambiarEstadoPersona(personaId, estadoAnterior));
            }

            return actualizado;
        }

        private static string GenerarNombreUsuarioBase(string nombreCompleto, int personaId)
        {
            string limpio = new string(nombreCompleto
                .ToLowerInvariant()
                .Where(c => char.IsLetterOrDigit(c))
                .Take(10)
                .ToArray());

            return string.IsNullOrWhiteSpace(limpio) ? $"usuario{personaId}" : $"{limpio}{personaId}";
        }
    }
}
