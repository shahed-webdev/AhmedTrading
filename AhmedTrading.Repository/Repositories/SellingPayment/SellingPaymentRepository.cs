﻿using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        PaidDate = model.PaidDate.ToLocalTime(),

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

        public async Task<DbResponse<int>> DuePayMultipleAsync(SellingDuePayMultipleModel model, IUnitOfWork db)
        {
            var response = new DbResponse<int>();
            try
            {
                var sells = await Context.Selling.Where(s => model.Bills.Select(i => i.SellingId).Contains(s.SellingId)).ToListAsync().ConfigureAwait(false);

                foreach (var invoice in model.Bills)
                {
                    var sell = sells.FirstOrDefault(s => s.SellingId == invoice.SellingId);
                    var due = (sell.SellingTotalPrice + sell.SellingReturnAmount + sell.TransportationCost) - sell.SellingPaidAmount;
                    if (due < invoice.SellingPaidAmount)
                    {
                        response.IsSuccess = false;
                        response.Message = $"{invoice.SellingPaidAmount} Paid amount is greater than due";
                        return response;
                    }
                    sell.SellingPaidAmount += invoice.SellingPaidAmount;
                }

                var Sn = await db.SellingPayments.GetNewSnAsync().ConfigureAwait(false);

                var receipt = new SellingPayment
                {
                    RegistrationId = model.RegistrationId,
                    CustomerId = model.CustomerId,
                    ReceiptSn = Sn,
                    PaidAmount = model.PaidAmount,
                    PaymentMethod = model.PaymentMethod,
                    PaidDate = model.PaidDate,
                    SellingPaymentList = model.Bills.Select(i => new SellingPaymentList
                    {
                        SellingId = i.SellingId,
                        SellingPaidAmount = i.SellingPaidAmount,
                    }).ToList()
                };

                Context.SellingPayment.Add(receipt);
                Context.Selling.UpdateRange(sells);

                await db.SaveChangesAsync().ConfigureAwait(false);

                db.Customers.UpdatePaidDue(model.CustomerId);

                response.IsSuccess = true;
                response.Message = "Success";
                response.Data = receipt.SellingPaymentId;
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.IsSuccess = false;
                return response;
            }
        }

        public Task<PaymentReceiptModel> ReceiptAsync(int id, IUnitOfWork db)
        {
            var receipt = Context.SellingPayment
                .Where(r => r.SellingPaymentId == id)
                .Select(s => new PaymentReceiptModel
                {
                    Invoices = s.SellingPaymentList.Select(p => new PaidInvoiceModel
                    {
                        SellingId = p.SellingId,
                        SellingSn = p.Selling.SellingSn,
                        SellingAmount = p.Selling.SellingTotalPrice,
                        SellingPaidAmount = p.SellingPaidAmount,
                        SellingDate = p.Selling.SellingDate
                    }).ToList(),

                    CustomerInfo = new CustomerReceiptViewModel
                    {
                        CustomerId = s.Customer.CustomerId,
                        CustomerName = s.Customer.CustomerName,
                        CustomerAddress = s.Customer.CustomerAddress,
                        CustomerPhone = s.Customer.CustomerPhone.FirstOrDefault().Phone
                    },
                    InstitutionInfo = db.Institutions.FindCustom(),
                    SellingPaymentId = s.SellingPaymentId,
                    PaidDate = s.PaidDate,
                    ReceiptSn = s.ReceiptSn,
                    PaidAmount = s.PaidAmount,
                    PaymentMethod = s.PaymentMethod,
                    CollectBy = s.Registration.Name
                }).FirstOrDefaultAsync();

            return receipt;
        }

        public DataResult<SellingPaymentModel> ReceiptDataTable(DataRequest request)
        {
            return Context.SellingPayment.Select(s => new SellingPaymentModel
            {
                SellingPaymentId = s.SellingPaymentId,
                CustomerId = s.CustomerId,
                CustomerName = string.IsNullOrEmpty(s.Customer.CustomerName) ? "CASH SALE" : s.Customer.CustomerName,
                PaidDate = s.PaidDate,
                ReceiptSn = s.ReceiptSn,
                PaidAmount = s.PaidAmount,
                PaymentMethod = s.PaymentMethod
            }).ToDataResult(request);
        }

        public DbResponse DeleteReceipt(int id, IUnitOfWork db)
        {
            try
            {

                var sellingPayment = Context.SellingPayment
                    .Include(s => s.SellingPaymentList)
                    .ThenInclude(l => l.Selling)
                    .FirstOrDefault(s => s.SellingPaymentId == id);

                if (sellingPayment == null) return new DbResponse(false, "No Data Found");

                //Payment Delete

                foreach (var list in sellingPayment.SellingPaymentList)
                {
                    var selling = list.Selling;

                    selling.SellingPaidAmount -= list.SellingPaidAmount;
                    Context.SellingPaymentList.Remove(list);
                    Context.Selling.Update(selling);

                }
                Context.SellingPayment.Remove(sellingPayment);
                Context.SaveChanges();

                //Customer balance Update
                if (sellingPayment.CustomerId != null)
                {
                    db.Customers.UpdatePaidDue(sellingPayment.CustomerId);
                }

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public double DateWiseSalePayment(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.SellingPayment
                       .Where(p => p.PaidDate <= tD && p.PaidDate >= fD)?
                       .Sum(p => p.PaidAmount) ?? 0;
        }

        public double DateWiseCashSale(DateTime? fromDate, DateTime? toDate)
        {
            var fD = fromDate ?? new DateTime(1000, 1, 1);
            var tD = toDate ?? new DateTime(3000, 12, 31);

            return Context.SellingPaymentList
                       .Where(p => p.Selling.SellingDate <= tD && p.Selling.SellingDate >= fD)?
                       .Sum(p => p.SellingPaidAmount) ?? 0;
        }
    }
}