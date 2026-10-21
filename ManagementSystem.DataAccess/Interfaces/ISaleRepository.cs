using System.Collections.Generic;
using ManagementSystem.Entities.Entities;

namespace ManagementSystem.DataAccess.Interfaces
{
    public interface ISaleRepository
    {
        int RegisterSaleWithTransaction(Sale sale, IEnumerable<SaleDetail> details, int employeeId);
        IEnumerable<Sale> GetAllSales();
        IEnumerable<SaleDetail> GetSaleDetails(int saleId);
    }
}
