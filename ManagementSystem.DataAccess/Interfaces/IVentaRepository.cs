using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface IVentaRepository
    {
        int RegisterSaleWithTransaction(Venta sale, IEnumerable<DetalleVenta> details, int employeeId);
        Venta? GetSaleById(int saleId);
        IEnumerable<Venta> GetAllSales();
        IEnumerable<DetalleVenta> GetSaleDetails(int saleId);
    }

    /// <summary>
    /// Alias de compatibilidad con ISaleRepository.
    /// </summary>
    public interface ISaleRepository : IVentaRepository
    {
    }
}
