using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class VendorCommissionRepository : Repository<VendorCommission>, IVendorCommissionRepository
    {
        public VendorCommissionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddCustom(VendorCommissionAddModel model)
        {
            if (Context.VendorCommission.Any(c => c.VendorId == model.VendorId && c.ProductId == model.ProductId && c.MonthDate.Year == model.MonthDate.Year && c.MonthDate.Month == c.MonthDate.Month))
                throw new Exception("Already commission added");

            var commission = new VendorCommission
            {
                VendorId = model.VendorId,
                ProductId = model.ProductId,
                Commission = model.Commission,
                MonthDate = model.MonthDate
            };

            Context.VendorCommission.Add(commission);
            var vendor = Context.Vendor.Find(model.VendorId);
            vendor.Commission += model.Commission;
            Context.Vendor.Update(vendor);

        }

        public Task<List<VendorCommissionViewModel>> ListAsync(int vendorId = 0)
        {
            var commission = Context.VendorCommission.Include(v => v.Product).Where(v => v.VendorId == vendorId)
                .Select(v => new VendorCommissionViewModel
                {
                    MonthName = v.MonthDate.ToString("MMMM"),
                    ProductName = v.Product.ProductName,
                    Commission = v.Commission,
                    MonthNumber = v.MonthDate.Month,
                    Year = v.MonthDate.Year
                }).ToListAsync();
            return commission;
        }

        public DataResult<VendorCommissionViewModel> ListDataTable(DataRequest request)
        {
            var commission = Context.VendorCommission.Include(v => v.Product)
                .Select(v => new VendorCommissionViewModel
                {
                    MonthName = v.MonthDate.ToString("MMMM"),
                    ProductName = v.Product.ProductName,
                    Commission = v.Commission,
                    MonthNumber = v.MonthDate.Month,
                    Year = v.MonthDate.Year
                });
            return commission.ToDataResult(request);
        }

        public void RemoveCustom(int id)
        {
            var commission = Context.VendorCommission.Find(id);
            if (commission == null) throw new Exception("Commission not found");
            var vendor = Context.Vendor.Find(commission.VendorId);
            vendor.Commission -= commission.Commission;
            Context.VendorCommission.Remove(commission);
            Context.Vendor.Update(vendor);
        }
    }
}