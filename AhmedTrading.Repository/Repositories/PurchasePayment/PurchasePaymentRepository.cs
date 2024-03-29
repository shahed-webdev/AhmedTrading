﻿using AhmedTrading.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class PurchasePaymentRepository : Repository<PurchasePayment>, IPurchasePaymentRepository
    {
        public PurchasePaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetNewSnAsync()
        {
            var sn = 0;
            if (await Context.PurchasePayment.AnyAsync().ConfigureAwait(false))
            {
                sn = await Context.PurchasePayment.MaxAsync(p => p == null ? 0 : p.ReceiptSn).ConfigureAwait(false);
            }

            return sn + 1;
        }

        public async Task<DbResponse> DuePaySingleAsync(PurchaseDuePaySingleModel model, IUnitOfWork db)
        {
            var response = new DbResponse();
            try
            {
                var Sn = await db.PurchasePayments.GetNewSnAsync().ConfigureAwait(false);

                var purchase = await Context.Purchase.FindAsync(model.PurchaseId).ConfigureAwait(false);



                var due = (purchase.PurchaseTotalPrice + purchase.PurchaseReturnAmount) -
                    (purchase.PurchaseDiscountAmount + purchase.PurchasePaidAmount);
                if (model.PaidAmount > due)
                {
                    response.IsSuccess = false;
                    response.Message = "Paid amount is greater than due";
                }

                if (model.PaidAmount > 0)
                {
                    var PurchasePayment = new PurchasePayment
                    {
                        RegistrationId = model.RegistrationId,
                        VendorId = model.VendorId,
                        ReceiptSn = Sn,
                        PaidAmount = model.PaidAmount,
                        PaymentMethod = model.PaymentMethod,
                        PaidDate = model.PaidDate,

                        PurchasePaymentList = new List<PurchasePaymentList>
                        {
                            new PurchasePaymentList
                            {
                                PurchaseId = model.PurchaseId,
                                PurchasePaidAmount = model.PaidAmount
                            }
                        }
                    };
                    await Context.PurchasePayment.AddAsync(PurchasePayment).ConfigureAwait(false);
                }

                purchase.PurchasePaidAmount += model.PaidAmount;

                Context.Purchase.Update(purchase);

                await db.SaveChangesAsync().ConfigureAwait(false);

                db.Vendors.UpdatePaidDue(model.VendorId);
                db.SaveChanges();

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

        public double DateWisePurchasePayment(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.PurchasePayment
                       .Where(p => p.PaidDate <= tD && p.PaidDate >= fD)?
                       .Sum(p => p.PaidAmount) ?? 0;
        }
    }
}