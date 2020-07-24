using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class AdvanceRepository : Repository<Advance>, IAdvanceRepository
    {
        public AdvanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse Add(AdvanceAddModel model)
        {
            try
            {
                if (IsNameExist(model.AdvanceName)) return new DbResponse(false, "Advance name already exist");

                var advance = new Advance
                {
                    AdvanceId = model.AdvanceId,
                    AdvanceName = model.AdvanceName,
                    AdvanceFor = model.AdvanceFor,
                    AdvanceAmount = model.AdvanceAmount,
                    TimePeriod = model.TimePeriod,
                    AdvanceDate = model.AdvanceDate
                };

                Context.Advance.Add(advance);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<AdvanceViewModel> ListDataTable(DataRequest request)
        {
            var list = Context.Advance.Select(a => new AdvanceViewModel
            {
                AdvanceId = a.AdvanceId,
                AdvanceName = a.AdvanceName,
                AdvanceFor = a.AdvanceFor,
                AdvanceAmount = a.AdvanceAmount,
                TimePeriod = a.TimePeriod,
                AdvanceDate = a.AdvanceDate
            });
            return list.ToDataResult(request);
        }

        public bool IsNameExist(string name)
        {
            return Context.Advance.Any(a => a.AdvanceName == name);
        }

        public bool IsNameExist(string name, int UpdateId)
        {
            return Context.Advance.Any(a => a.AdvanceName == name && a.AdvanceId != UpdateId);
        }

        public DbResponse<AdvanceViewModel> Details(int id)
        {
            try
            {
                var advance = Context.Advance.Find(id);
                if (advance == null) return new DbResponse<AdvanceViewModel>(false, "No Data Found");

                var advanceView = new AdvanceViewModel
                {
                    AdvanceId = advance.AdvanceId,
                    AdvanceName = advance.AdvanceName,
                    AdvanceFor = advance.AdvanceFor,
                    AdvanceAmount = advance.AdvanceAmount,
                    TimePeriod = advance.TimePeriod,
                    AdvanceDate = advance.AdvanceDate
                };
                return new DbResponse<AdvanceViewModel>(true, "Success") { Data = advanceView };
            }
            catch (Exception e)
            {
                return new DbResponse<AdvanceViewModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                var advance = Context.Advance.Find(id);
                if (advance == null) return new DbResponse(false, "No Data Found");

                Context.Advance.Remove(advance);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public double TotalAdvance()
        {
            return Context.Advance?.Sum(a => a.AdvanceAmount) ?? 0;
        }
    }
}