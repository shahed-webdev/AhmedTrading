using AhmedTrading.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class SellingPaymentRepository : Repository<SellingPayment>, ISellingPaymentRepository
    {
        public SellingPaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetNewSnAsync()
        {
            var sn = 0;
            if (await Context.SellingPayment.AnyAsync().ConfigureAwait(false))
            {
                sn = await Context.SellingPayment.MaxAsync(p => p == null ? 0 : p.ReceiptSn).ConfigureAwait(false);
            }

            return sn + 1;
        }

        public async Task<DbResponse> DuePaySingleAsync(SellingDuePaySingleModel model, IUnitOfWork db)
        {
            var response = new DbResponse();
            try
            {
                var Sn = await db.SellingPayments.GetNewSnAsync().ConfigureAwait(false);

                var selling = await Context.Selling.FindAsync(model.SellingId).ConfigureAwait(false);



                var due = (selling.SellingTotalPrice + selling.SellingReturnAmount) -
                    (model.SellingDiscountAmount + selling.SellingPaidAmount);
                if (model.PaidAmount > due)
                {
                    response.IsSuccess = false;
                    response.Message = "Paid amount is greater than due";
                }

                if (model.PaidAmount > 0)
                {
                    var sellingPayment = new SellingPayment
                    {
                        RegistrationId = model.RegistrationId,
                        CustomerId = model.CustomerId,
                        ReceiptSn = Sn,
                        PaidAmount = model.PaidAmount,
                        PaymentMethod = model.PaymentMethod,
                        PaidDate = model.PaidDate,

                        SellingPaymentList = new List<SellingPaymentList>
                        {
                            new SellingPaymentList
                            {
                                SellingId = model.SellingId,
                                SellingPaidAmount = model.PaidAmount
                            }
                        }
                    };
                    await Context.SellingPayment.AddAsync(sellingPayment).ConfigureAwait(false);
                }

                selling.SellingDiscountAmount = model.SellingDiscountAmount;
                selling.SellingPaidAmount += model.PaidAmount;

                Context.Selling.Update(selling);

                await db.SaveChangesAsync().ConfigureAwait(false);

                db.Customers.UpdatePaidDue(model.CustomerId);

                response.IsSuccess = true;
                response.Message = "Success";
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.IsSuccess = false;
                return response;
            }
        }
    }
}