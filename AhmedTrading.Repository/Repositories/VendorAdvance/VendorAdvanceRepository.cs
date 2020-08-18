using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class VendorAdvanceRepository : Repository<VendorAdvance>, IVendorAdvanceRepository
    {
        public VendorAdvanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void AddCustom(VendorAdvanceAddViewModel model)
        {
            var advance = new VendorAdvance
            {
                VendorId = model.VendorId,
                Advance = model.Advance,
                AdvanceDate = model.AdvanceDate,
                PaymentMethod = model.PaymentMethod,
                AdvanceDetails = model.AdvanceDetails,
                InsertDate = model.AdvanceDate
            };
            Context.VendorAdvance.Add(advance);

            var vendor = Context.Vendor.Find(model.VendorId);
            vendor.Advance += model.Advance;
            Context.Vendor.Update(vendor);

        }

        public Task<List<VendorAdvanceRecordViewModel>> VendorWiseRecords(int vendorId)
        {
            var records = Context.VendorAdvance.Where(v => v.VendorId == vendorId)
                .Select(v => new VendorAdvanceRecordViewModel
                {
                    VendorAdvanceId = v.VendorAdvanceId,
                    Advance = v.Advance,
                    AdvanceDate = v.AdvanceDate,
                    PaymentMethod = v.PaymentMethod,
                    AdvanceDetails = v.AdvanceDetails
                }).ToListAsync();

            return records;
        }

        public DataResult<VendorAdvanceRecordViewModel> VendorWiseRecordsDataTable(DataRequest request)
        {
            var records = Context.VendorAdvance
                .Select(v => new VendorAdvanceRecordViewModel
                {
                    VendorAdvanceId = v.VendorAdvanceId,
                    Advance = v.Advance,
                    AdvanceDate = v.AdvanceDate,
                    PaymentMethod = v.PaymentMethod,
                    AdvanceDetails = v.AdvanceDetails
                });

            return records.ToDataResult(request);
        }

        public void RemoveCustom(int id)
        {
            var advance = Context.VendorAdvance.Find(id);
            if (advance == null) return;

            var vendor = Context.Vendor.Find(advance.VendorId);
            vendor.Advance -= advance.Advance;

            Context.VendorAdvance.Remove(advance);
            Context.Vendor.Update(vendor);

        }

        public double DateWiseVendorAdvance(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.VendorAdvance
                       .Where(p => p.AdvanceDate <= tD && p.AdvanceDate >= fD)?
                       .Sum(p => p.Advance) ?? 0;
        }
    }
}