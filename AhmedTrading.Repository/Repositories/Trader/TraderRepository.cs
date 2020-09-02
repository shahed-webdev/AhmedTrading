using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class TraderRepository : Repository<Trader>, ITraderRepository
    {
        public TraderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse Add(TraderModel model)
        {
            try
            {
                if (IsPhoneExist(model.Phone)) return new DbResponse(false, "Trader already exist");

                var trader = new Trader
                {
                    TraderName = model.TraderName,
                    Phone = model.Phone
                };

                Context.Trader.Add(trader);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<TraderDetailsModel> ListDataTable(DataRequest request)
        {
            return Context.Trader.Select(t => new TraderDetailsModel
            {
                TraderId = t.TraderId,
                TraderName = t.TraderName,
                Phone = t.Phone,
                GivenProductPrice = t.GivenProductPrice,
                GivenAmount = t.GivenAmount,
                TakenProductPrice = t.TakenProductPrice,
                TakenAmount = t.TakenAmount,
                NetAmount = t.NetAmount
            }).ToDataResult(request);
        }

        public bool IsPhoneExist(string phone)
        {
            return Context.Trader.Any(c => c.Phone == phone);
        }

        public bool IsPhoneExist(string phone, int updateId)
        {
            return Context.Person.Any(c => c.Phone == phone && c.PersonId != updateId);
        }

        public DbResponse<TraderDetailsModel> Details(int id)
        {
            try
            {
                var t = Context.Trader
                    .Include(p => p.TraderSharing)
                    .Include(p => p.TraderSharingPayment)
                    .FirstOrDefault(p => p.TraderId == id);
                if (t == null) return new DbResponse<TraderDetailsModel>(false, "No Data Found");

                var traderModel = new TraderDetailsModel
                {
                    TraderId = t.TraderId,
                    TraderName = t.TraderName,
                    Phone = t.Phone,
                    GivenProductPrice = t.GivenProductPrice,
                    GivenAmount = t.GivenAmount,
                    TakenProductPrice = t.TakenProductPrice,
                    TakenAmount = t.TakenAmount,
                    NetAmount = t.NetAmount
                };
                return new DbResponse<TraderDetailsModel>(true, "Success") { Data = traderModel };
            }
            catch (Exception e)
            {
                return new DbResponse<TraderDetailsModel>(false, e.Message);
            }
        }

        public DbResponse Delete(int id)
        {
            try
            {
                var p = Context.Trader.Find(id);
                if (p == null) return new DbResponse(false, "No Data Found");
                if (Context.TraderSharing.Any(t => t.TraderId == p.TraderId)) return new DbResponse(false, "Related Data Found");

                Context.Trader.Remove(p);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public async Task<ICollection<TraderModel>> SearchAsync(string key)
        {
            return await Context.Trader
                .Where(c => c.TraderName.Contains(key) || c.Phone.Contains(key))
                .Select(p =>
                    new TraderModel
                    {
                        TraderId = p.TraderId,
                        TraderName = p.TraderName,
                        Phone = p.Phone
                    }).Take(5).ToListAsync().ConfigureAwait(false);
        }
    }
}