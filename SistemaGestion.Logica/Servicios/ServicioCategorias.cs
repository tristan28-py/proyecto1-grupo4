using System;
using System.Collections.Generic;
using System.Linq;
using ManagementSystem.DataAccess.ADONET;
using ManagementSystem.DataAccess.Interfaces;
using ManagementSystem.Entities.Entities;
using SistemaGestion.Logica.Interfaces;

namespace SistemaGestion.Logica.Servicios
{
    /// <summary>
    /// Servicio de lógica de negocio para la gestión de Categorías.
    /// Implementa CRUD y consulta por estado utilizando ADO.NET y LINQ sobre List&lt;Categoria&gt;.
    /// </summary>
    public class ServicioCategorias : IServicioCategorias
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IServicioHistorial _servicioHistorial;

        public ServicioCategorias()
            : this(new CategoriaRepositorioAdo(), ServicioHistorial.InstanciaCompartida)
        {
        }

        public ServicioCategorias(ICategoriaRepositorio categoriaRepositorio, IServicioHistorial? servicioHistorial = null)
        {
            _categoriaRepositorio = categoriaRepositorio ?? throw new ArgumentNullException(nameof(categoriaRepositorio));
            _servicioHistorial = servicioHistorial ?? ServicioHistorial.InstanciaCompartida;
        }

        public List<Categoria> ObtenerTodas()
        {
            return _categoriaRepositorio.ObtenerTodos().OrderBy(c => c.Nombre).ToList();
        }

        public List<Categoria> ObtenerActivas()
        {
            return _categoriaRepositorio.ObtenerActivas().OrderBy(c => c.Nombre).ToList();
        }

        public Categoria? ObtenerPorId(int categoriaId)
        {
            return categoriaId > 0 ? _categoriaRepositorio.ObtenerPorId(categoriaId) : null;
        }

        /// <summary>
        /// Filtra categorías por coincidencia de texto y por estado (0 = Todos, 1 = Solo Activos, 2 = Solo Inactivos).
        /// </summary>
        public List<Categoria> FiltrarCategorias(string? textoBusqueda, int indiceEstado)
        {
            List<Categoria> categorias = ObtenerTodas();
            IEnumerable<Categoria> consulta = categorias;

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string filtro = textoBusqueda.Trim();
                consulta = consulta.Where(c =>
                    c.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    (c.Descripcion != null && c.Descripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase)));
            }

            if (indiceEstado == 1)
            {
                consulta = consulta.Where(c => c.Activo);
            }
            else if (indiceEstado == 2)
            {
                consulta = consulta.Where(c => !c.Activo);
            }

            return consulta.OrderBy(c => c.Nombre).ToList();
        }

        public bool ValidarCategoria(Categoria categoria, out List<string> errores)
        {
            errores = new List<string>();
            if (categoria == null)
            {
                errores.Add("Los datos de la categoría son obligatorios.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                errores.Add("El nombre de la categoría es obligatorio.");
            }

            return errores.Count == 0;
        }

        public int AgregarCategoria(Categoria categoria)
        {
            if (!ValidarCategoria(categoria, out List<string> errores))
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errores));
            }

            int nuevoId = _categoriaRepositorio.Insertar(categoria);
            categoria.CategoriaId = nuevoId;

            _servicioHistorial.RegistrarAccion(
                modulo: "Categorías",
                descripcion: $"Se registró la categoría '{categoria.Nombre}' (ID #{nuevoId})",
                tipoAccion: "Creación",
                entidadId: nuevoId,
                accionDeshacer: () => _categoriaRepositorio.Eliminar(nuevoId));

            return nuevoId;
        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            if (!ValidarCategoria(categoria, out List<string> errores))
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errores));
            }

            bool actualizado = _categoriaRepositorio.Actualizar(categoria);
            if (actualizado)
            {
                _servicioHistorial.RegistrarAccion(
                    modulo: "Categorías",
                    descripcion: $"Se actualizó la categoría '{categoria.Nombre}' (ID #{categoria.CategoriaId})",
                    tipoAccion: "Actualización",
                    entidadId: categoria.CategoriaId);
            }

            return actualizado;
        }

        public bool CambiarEstadoCategoria(int categoriaId, bool nuevoEstado)
        {
            Categoria? existente = _categoriaRepositorio.ObtenerPorId(categoriaId);
            if (existente == null)
            {
                return false;
            }

            bool estadoAnterior = existente.Activo;
            existente.Activo = nuevoEstado;
            bool resultado = _categoriaRepositorio.Actualizar(existente);

            if (resultado)
            {
                string estadoTexto = nuevoEstado ? "Activa" : "Inactiva";
                _servicioHistorial.RegistrarAccion(
                    modulo: "Categorías",
                    descripcion: $"Se cambió el estado de la categoría '{existente.Nombre}' a {estadoTexto}",
                    tipoAccion: "Estado",
                    entidadId: categoriaId,
                    accionDeshacer: () =>
                    {
                        existente.Activo = estadoAnterior;
                        _categoriaRepositorio.Actualizar(existente);
                    });
            }

            return resultado;
        }

        public bool EliminarCategoria(int categoriaId)
        {
            return CambiarEstadoCategoria(categoriaId, false);
        }
    }
}
