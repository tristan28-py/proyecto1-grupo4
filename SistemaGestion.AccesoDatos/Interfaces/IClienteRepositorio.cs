using System.Collections.Generic;
using SistemaGestion.Entidades.Entidades;

namespace SistemaGestion.AccesoDatos.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Cliente. Hereda de IRepositorio&lt;Cliente&gt;.
    /// </summary>
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        IEnumerable<Cliente> BuscarClientes(string termino);
    }
}
