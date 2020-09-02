using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class TraderSharingRepository : Repository<TraderSharing>, ITraderSharingRepository
    {
        public TraderSharingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse Add(TraderSharingAddModel model)
        {
            try
            {
                if (!Context.Product.Any(p => p.ProductId == model.ProductId)) return new DbResponse(false, "Product not found");
                if (!Context.Trader.Any(t => t.TraderId == model.TraderId)) return new DbResponse(false, "Trader not found");

                var traderSharing = new TraderSharing
                {
                    TraderId = model.TraderId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    UnitPrice = model.UnitPrice,
                    IsGiven = model.IsGiven,
                    ShareDate = model.ShareDate.ToLocalTime(),
                };

                Context.TraderSharing.Add(traderSharing);

                //update Trader
                var trader = Context.Trader.Find(model.TraderId);

                if (model.IsGiven)
                {
                    trader.GivenProductPrice += (model.Quantity * model.UnitPrice);
                }
                else
                {
                    trader.TakenProductPrice += (model.Quantity * model.UnitPrice);
                }

                Context.Trader.Update(trader);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DataResult<TraderSharingDetailsModel> GivenDataTable(DataRequest request)
        {
            return Context.TraderSharing
                .Include(t => t.Trader)
                .Include(t => t.Product)
                .Where(t => t.IsGiven)
                .Select(t => new TraderSharingDetailsModel
                {
                    TraderSharingId = t.TraderSharingId,
                    TraderId = t.TraderId,
                    TraderName = t.Trader.TraderName,
                    ProductId = t.ProductId,
                    ProductName = t.Product.ProductName,
                    Quantity = t.Quantity,
                    UnitPrice = t.UnitPrice,
                    SharePrice = t.SharePrice,
                    IsGiven = t.IsGiven,
                    ShareDate = t.ShareDate
                }).ToDataResult(request);
        }

        public DataResult<TraderSharingDetailsModel> TakenDataTable(DataRequest request)
        {
            return Context.TraderSharing
                .Include(t => t.Trader)
                .Include(t => t.Product)
                .Where(t => !t.IsGiven)
                .Select(t => new TraderSharingDetailsModel
                {
                    TraderSharingId = t.TraderSharingId,
                    TraderId = t.TraderId,
                    TraderName = t.Trader.TraderName,
                    ProductId = t.ProductId,
                    ProductName = t.Product.ProductName,
                    Quantity = t.Quantity,
                    UnitPrice = t.UnitPrice,
                    SharePrice = t.SharePrice,
                    IsGiven = t.IsGiven,
                    ShareDate = t.ShareDate
                }).ToDataResult(request);
        }

        public DbResponse Delete(int id)
        {
            try
            {
                var traderSharing = Context.TraderSharing.Find(id);
                if (traderSharing == null) return new DbResponse(false, "No Data Found");


                Context.TraderSharing.Remove(traderSharing);

                //update Trader
                var trader = Context.Trader.Find(traderSharing.TraderId);

                if (traderSharing.IsGiven)
                {
                    trader.GivenProductPrice -= (traderSharing.Quantity * traderSharing.UnitPrice);
                }
                else
                {
                    trader.TakenProductPrice -= (traderSharing.Quantity * traderSharing.UnitPrice);
                }

                Context.Trader.Update(trader);

                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}