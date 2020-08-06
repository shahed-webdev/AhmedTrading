using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<VendorViewModel>> ToListCustomAsync()
        {
            var vendor = await Context.Vendor.Select(v => new VendorViewModel
            {
                VendorId = v.VendorId,
                VendorCompanyName = v.VendorCompanyName,
                VendorName = v.VendorName,
                VendorAddress = v.VendorAddress,
                VendorPhone = v.VendorPhone,
                InsertDate = v.InsertDate,
                Balance = v.Balance,
                Advance = v.Advance,
                Commission = v.Commission,
                OpeningDue = v.OpeningDue
            }).ToListAsync().ConfigureAwait(false);

            return vendor;
        }

        public DataResult<VendorViewModel> ListDataTable(DataRequest request)
        {
            var vendor = Context.Vendor.Select(v => new VendorViewModel
            {
                VendorId = v.VendorId,
                VendorCompanyName = v.VendorCompanyName,
                VendorName = v.VendorName,
                VendorAddress = v.VendorAddress,
                VendorPhone = v.VendorPhone,
                InsertDate = v.InsertDate,
                Balance = v.Balance,
                Advance = v.Advance,
                Commission = v.Commission,
                OpeningDue = v.OpeningDue
            });
            return vendor.ToDataResult(request);
        }

        public async Task<ICollection<VendorViewModel>> SearchAsync(string key)
        {
            return await Context.Vendor.Where(v => v.VendorName.Contains(key) || v.VendorPhone.Contains(key) || v.VendorCompanyName.Contains(key)).Select(v =>
                new VendorViewModel
                {
                    VendorId = v.VendorId,
                    VendorCompanyName = v.VendorCompanyName,
                    VendorName = v.VendorName,
                    VendorAddress = v.VendorAddress,
                    VendorPhone = v.VendorPhone,
                    InsertDate = v.InsertDate,
                    Balance = v.Balance,
                    Advance = v.Advance,
                    Commission = v.Commission,
                    OpeningDue = v.OpeningDue
                }).Take(5).ToListAsync().ConfigureAwait(false);
        }

        public Vendor AddCustom(VendorViewModel model)
        {
            var vendor = new Vendor
            {
                VendorCompanyName = model.VendorCompanyName,
                VendorName = model.VendorName,
                VendorAddress = model.VendorAddress,
                VendorPhone = model.VendorPhone,
                OpeningDue = model.OpeningDue
            };

            Add(vendor);

            return vendor;
        }

        public void UpdateCustom(VendorViewModel model)
        {
            var vendor = Find(model.VendorId);

            vendor.VendorCompanyName = model.VendorCompanyName;
            vendor.VendorName = model.VendorName;
            vendor.VendorAddress = model.VendorAddress;
            vendor.VendorPhone = model.VendorPhone;
            vendor.OpeningDue = model.OpeningDue;
            Update(vendor);
        }

        public VendorViewModel FindCustom(int? id)
        {
            var vendor = Find(id.GetValueOrDefault());
            if (vendor == null) return null;

            return new VendorViewModel
            {
                VendorId = vendor.VendorId,
                VendorCompanyName = vendor.VendorCompanyName,
                VendorName = vendor.VendorName,
                VendorAddress = vendor.VendorAddress,
                VendorPhone = vendor.VendorPhone,
                InsertDate = vendor.InsertDate,
                Balance = vendor.Balance,
                Advance = vendor.Advance,
                Commission = vendor.Commission,
                OpeningDue = vendor.OpeningDue
            };
        }

        public void UpdatePaidDue(int id)
        {
            var vendor = Find(id);
            var obj = Context.Purchase.Where(p => p.VendorId == vendor.VendorId).GroupBy(pg => pg.VendorId).Select(p =>
                new
                {
                    TotalAmount = p.Sum(c => c.PurchaseTotalPrice),
                    TotalDiscount = p.Sum(c => c.PurchaseDiscountAmount),
                    Paid = p.Sum(c => c.PurchasePaidAmount)
                }).FirstOrDefault();

            vendor.TotalAmount = obj.TotalAmount;
            vendor.TotalDiscount = obj.TotalDiscount;
            vendor.Paid = obj.Paid;

            Update(vendor);
        }

        public bool RemoveCustom(int id)
        {
            //if (Context.Selling.Any(s => s.VendorID == id)) return false;
            Remove(Find(id));
            return true;
        }

        public double TotalDue()
        {
            return Context.Vendor?.Sum(v => v.Balance) ?? 0;
        }

        public VendorProfileViewModel ProfileDetails(int id, IUnitOfWork db)
        {
            var vendor = Find(id);
            if (vendor == null) return null;
            var products = db.Products.FindByVendor(vendor.VendorId);

            return new VendorProfileViewModel
            {
                VendorId = vendor.VendorId,
                VendorCompanyName = vendor.VendorCompanyName,
                VendorName = vendor.VendorName,
                VendorAddress = vendor.VendorAddress,
                VendorPhone = vendor.VendorPhone,
                Balance = vendor.Balance,
                Advance = vendor.Advance,
                Commission = vendor.Commission,
                Paid = vendor.Paid,
                ReturnAmount = vendor.ReturnAmount,
                TotalAmount = vendor.TotalAmount,
                TotalDiscount = vendor.TotalDiscount,
                Products = products

            };
        }
    }
}
