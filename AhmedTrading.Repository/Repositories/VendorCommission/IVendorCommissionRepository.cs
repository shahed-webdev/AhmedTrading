using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IVendorCommissionRepository : IRepository<VendorCommission>, IAddCustom<VendorCommissionAddModel>
    {
        Task<List<VendorCommissionViewModel>> ListAsync(int vendorId = 0);
        DataResult<VendorCommissionViewModel> ListDataTable(DataRequest request);
        void RemoveCustom(int id);
        double TotalCommission();
    }
}