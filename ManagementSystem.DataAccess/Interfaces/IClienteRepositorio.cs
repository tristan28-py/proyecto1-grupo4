using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    /// <summary>
    /// Contrato de repositorio para la entidad Cliente. Hereda de IRepositorio&lt;Cliente&gt;.
    /// </summary>
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        IEnumerable<Cliente> BuscarClientes(string termino);
    }
}
