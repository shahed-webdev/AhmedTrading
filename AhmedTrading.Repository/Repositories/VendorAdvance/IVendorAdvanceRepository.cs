using AhmedTrading.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IVendorAdvanceRepository : IRepository<VendorAdvance>, IAddCustom<VendorAdvanceAddViewModel>
    {
        Task<List<VendorAdvanceRecordViewModel>> VendorWiseRecords(int vendorId);
        void RemoveCustom(int id);
    }
}