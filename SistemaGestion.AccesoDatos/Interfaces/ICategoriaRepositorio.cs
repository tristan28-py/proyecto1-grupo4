using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Categoria. Hereda de IRepositorio&lt;Categoria&gt;.
    /// </summary>
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        IEnumerable<Categoria> ObtenerActivas();
    }
}
