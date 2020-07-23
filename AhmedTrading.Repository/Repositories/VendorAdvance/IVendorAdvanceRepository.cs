using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IVendorAdvanceRepository : IRepository<VendorAdvance>, IAddCustom<VendorAdvanceAddViewModel>
    {
        Task<List<VendorAdvanceRecordViewModel>> VendorWiseRecords(int vendorId);
        DataResult<VendorAdvanceRecordViewModel> VendorWiseRecordsDataTable(DataRequest request);
        void RemoveCustom(int id);
    }
}