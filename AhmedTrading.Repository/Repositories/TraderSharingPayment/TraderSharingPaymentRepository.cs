using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AhmedTrading.Repository
{
    public class TraderSharingPaymentRepository : Repository<TraderSharingPayment>, ITraderSharingPaymentRepository
    {
        public TraderSharingPaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse Add(TraderSharingPaymentAddModel model)
        {
            try
            {
                if (!Context.Trader.Any(t => t.TraderId == model.TraderId)) return new DbResponse(false, "Trader not found");

                var traderSharingPayment = new TraderSharingPayment
                {
                    TraderId = model.TraderId,
                    Amount = model.Amount,
                    PaymentMethod = model.PaymentMethod,
                    IsGiven = model.IsGiven,
                    PaymentDate = model.PaymentDate.ToLocalTime(),
                };

                Context.TraderSharingPayment.Add(traderSharingPayment);

                //update Trader
                var trader = Context.Trader.Find(model.TraderId);

                if (model.IsGiven)
                {
                    trader.GivenAmount += model.Amount;
                }
                else
                {
                    trader.TakenAmount += model.Amount;
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

        public DataResult<TraderSharingPaymentDetailsModel> GivenDataTable(DataRequest request)
        {
            return Context.TraderSharingPayment
                .Include(t => t.Trader)
                .Where(t => t.IsGiven)
                .Select(t => new TraderSharingPaymentDetailsModel
                {
                    TraderId = t.TraderId,
                    TraderName = t.Trader.TraderName,
                    Amount = t.Amount,
                    PaymentMethod = t.PaymentMethod,
                    IsGiven = t.IsGiven,
                    PaymentDate = t.PaymentDate
                }).ToDataResult(request);
        }

        public DataResult<TraderSharingPaymentDetailsModel> TakenDataTable(DataRequest request)
        {
            return Context.TraderSharingPayment
                .Include(t => t.Trader)
                .Where(t => !t.IsGiven)
                .Select(t => new TraderSharingPaymentDetailsModel
                {
                    TraderId = t.TraderId,
                    TraderName = t.Trader.TraderName,
                    Amount = t.Amount,
                    PaymentMethod = t.PaymentMethod,
                    IsGiven = t.IsGiven,
                    PaymentDate = t.PaymentDate
                }).ToDataResult(request);
        }

        public DbResponse Delete(int id)
        {
            try
            {
                var traderSharingPayment = Context.TraderSharingPayment.Find(id);
                if (traderSharingPayment == null) return new DbResponse(false, "No Data Found");


                Context.TraderSharingPayment.Remove(traderSharingPayment);

                //update Trader
                var trader = Context.Trader.Find(traderSharingPayment.TraderId);

                if (traderSharingPayment.IsGiven)
                {
                    trader.GivenAmount -= traderSharingPayment.Amount;
                }
                else
                {
                    trader.TakenAmount -= traderSharingPayment.Amount;
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