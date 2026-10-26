using System;
using System.Collections.Generic;

namespace SistemaGestion.Entidades.Entidades
{
    /// <summary>
    /// Representa una categoría utilizada para clasificar los productos del inventario.
    /// </summary>
    public class Categoria : IEntidadActivable
    {
        private int _categoriaId;
        private string _nombre = string.Empty;
        private string? _descripcion;
        private DateTime _fechaCreacion = DateTime.Now;
        private bool _activo = true;

        public int CategoriaId
        {
            get => _categoriaId;
            set => _categoriaId = value;
        }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value?.Trim() ?? string.Empty;
        }

        public string? Descripcion
        {
            get => _descripcion;
            set => _descripcion = value?.Trim();
        }

        public DateTime FechaCreacion
        {
            get => _fechaCreacion;
            set => _fechaCreacion = value;
        }

        public bool Activo
        {
            get => _activo;
            set => _activo = value;
        }

        // Propiedad de navegación (relación 1:N con Producto)
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

        public int ObtenerIdentificador() => _categoriaId;

        public void Activar() => _activo = true;

        public void Desactivar() => _activo = false;

        public override string ToString() => _nombre;
    }
}
