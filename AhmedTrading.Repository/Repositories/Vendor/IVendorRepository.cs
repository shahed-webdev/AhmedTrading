using AhmedTrading.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        Task<ICollection<VendorViewModel>> ToListCustomAsync();
        Task<ICollection<VendorViewModel>> SearchAsync(string key);
        Vendor AddCustom(VendorViewModel model);
        void UpdateCustom(VendorViewModel model);
        VendorViewModel FindCustom(int? id);
        void UpdatePaidDue(int id);
        bool RemoveCustom(int id);
        double TotalDue();
    }

}

