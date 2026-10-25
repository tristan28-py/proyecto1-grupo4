using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace SistemaGestion.Logica.Interfaces
{
    /// <summary>
    /// Contrato del servicio de negocio para la administración de Categorías.
    /// </summary>
    public interface IServicioCategorias
    {
        List<Categoria> ObtenerTodas();
        List<Categoria> ObtenerActivas();
        Categoria? ObtenerPorId(int categoriaId);
        List<Categoria> FiltrarCategorias(string? textoBusqueda, int indiceEstado);
        bool ValidarCategoria(Categoria categoria, out List<string> errores);
        int AgregarCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool CambiarEstadoCategoria(int categoriaId, bool nuevoEstado);
        bool EliminarCategoria(int categoriaId);
    }
}
