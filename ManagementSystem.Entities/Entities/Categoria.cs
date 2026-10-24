using System;
using System.Collections.Generic;

namespace ManagementSystem.Entities.Entities
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        // Propiedades de navegación (relación 1:N con Producto)
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
