using AhmedTrading.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IVendorCommissionRepository : IRepository<VendorCommission>, IAddCustom<VendorCommissionAddModel>
    {
        Task<List<VendorCommissionViewModel>> ListAsync(int vendorId = 0);
        void RemoveCustom(int id);
    }
}