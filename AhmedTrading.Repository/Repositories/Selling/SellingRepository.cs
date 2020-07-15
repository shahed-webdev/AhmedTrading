using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class SellingRepository : Repository<Selling>, ISellingRepository
    {
        public SellingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetNewSnAsync()
        {
            var sn = 0;
            if (await Context.Selling.AnyAsync().ConfigureAwait(false))
            {
                sn = await Context.Selling.MaxAsync(s => s == null ? 0 : s.SellingSn).ConfigureAwait(false);
            }

            return sn + 1;
        }

        public async Task<DbResponse<int>> AddCustomAsync(SellingViewModel model, IUnitOfWork db)
        {
            var response = new DbResponse<int>();


            var newSellingSn = await GetNewSnAsync().ConfigureAwait(false);
            var newSellingPaymentSn = await db.SellingPayments.GetNewSnAsync().ConfigureAwait(false);

            var selling = new Selling
            {
                RegistrationId = model.RegistrationId,
                CustomerId = model.CustomerId,
                SellingSn = newSellingSn,
                SellingTotalPrice = model.SellingTotalPrice,
                SellingDiscountAmount = model.SellingDiscountAmount,
                SellingDiscountPercentage = model.SellingDiscountAmount,
                SellingPaidAmount = model.SellingPaidAmount,
                SellingDate = model.SellingDate,
                SellingList = model.ProductList.Select(l => new SellingList
                {
                    ProductId = l.ProductId,
                    SellingUnitPrice = l.SellingUnitPrice,
                    SellingQuantity = l.SellingQuantity

                }).ToList(),
                SellingPaymentList = model.SellingPaidAmount > 0 ?
                    new List<SellingPaymentList>
                    {
                        new SellingPaymentList
                        {
                            SellingPaidAmount = model.SellingPaidAmount,
                            SellingPayment = new SellingPayment
                            {
                                SellingPaymentId = 0,
                                RegistrationId = model.RegistrationId,
                                CustomerId  = model.CustomerId,
                                ReceiptSn = newSellingPaymentSn,
                                PaidAmount = model.SellingPaidAmount,
                                PaymentMethod = model.PaymentMethod,
                                PaidDate = model.SellingDate
                            }
                        }
                    } : null
            };


            await Context.Selling.AddAsync(selling).ConfigureAwait(false);

            //Update Product Info
            foreach (var item in model.ProductList)
            {
                var product = Context.Product.Find(item.ProductId);
                product.Stock -= item.SellingQuantity;

                Context.Product.Update(product);
            }
            try
            {

                await Context.SaveChangesAsync().ConfigureAwait(false);

                response.IsSuccess = true;
                response.Message = "Success";
                response.Data = selling.SellingId;
            }
            catch (DbUpdateException e)
            {
                response.Message = e.Message;
                response.IsSuccess = false;
            }

            return response;
        }

        public Task<SellingReceiptViewModel> SellingReceiptAsync(int id, IUnitOfWork db)
        {
            var sellingReceipt = Context.Selling
              .Include(s => s.Customer)
              .ThenInclude(c => c.CustomerPhone)
              .Include(s => s.Registration)
              .Include(s => s.SellingList)
              .ThenInclude(p => p.Product)
              .ThenInclude(pd => pd.ProductBrand)
              .Include(s => s.SellingPaymentList)
              .ThenInclude(sp => sp.SellingPayment)
              .Select(s => new SellingReceiptViewModel
              {
                  SellingSn = s.SellingSn,
                  SellingId = s.SellingId,
                  SellingTotalPrice = s.SellingTotalPrice,
                  SellingDiscountAmount = s.SellingDiscountAmount,
                  SellingPaidAmount = s.SellingPaidAmount,
                  SellingDueAmount = s.SellingDueAmount,
                  SellingDate = s.SellingDate,
                  Products = s.SellingList.Select(pd => new SellingReceiptProductViewModel
                  {
                      ProductId = pd.Product.ProductId,
                      ProductBrandId = pd.Product.ProductBrandId,
                      BrandName = pd.Product.ProductBrand.BrandName,
                      ProductName = pd.Product.ProductName,
                      SellingUnitPrice = pd.SellingUnitPrice,
                      SellingQuantity = pd.SellingQuantity,
                      SellingPrice = pd.SellingPrice
                  }).ToList(),
                  Payments = s.SellingPaymentList.Select(pp => new SellingPaymentViewModel
                  {
                      PaymentMethod = pp.SellingPayment.PaymentMethod,
                      PaidAmount = pp.SellingPaidAmount,
                      PaidDate = pp.SellingPayment.PaidDate
                  }).ToList(),
                  CustomerInfo = new CustomerReceiptViewModel
                  {
                      CustomerId = s.Customer.CustomerId,
                      CustomerName = s.Customer.CustomerName,
                      CustomerAddress = s.Customer.CustomerAddress,
                      CustomerPhone = s.Customer.CustomerPhone.FirstOrDefault().Phone
                  },
                  InstitutionInfo = db.Institutions.FindCustom(),
                  SoildBy = s.Registration.Name
              }).FirstOrDefaultAsync(s => s.SellingId == id);

            return sellingReceipt;
        }

        public DataResult<SellingRecordViewModel> Records(DataRequest request)
        {
            var r = Context.Selling.Include(s => s.Customer).Select(s => new SellingRecordViewModel
            {
                SellingId = s.SellingId,
                CustomerId = s.CustomerId,
                CustomerName = s.Customer.CustomerName,
                SellingSn = s.SellingSn,
                SellingAmount = s.SellingTotalPrice,
                SellingPaidAmount = s.SellingPaidAmount,
                SellingDiscountAmount = s.SellingDiscountAmount,
                SellingDueAmount = s.SellingDueAmount,
                SellingDate = s.SellingDate
            });
            return r.ToDataResult(request);
        }

        public ICollection<int> Years()
        {
            var years = Context.Selling
                .GroupBy(e => new
                {
                    e.SellingDate.Year
                })
                .Select(g => g.Key.Year)
                .OrderBy(o => o)
                .ToList();

            var currentYear = DateTime.Now.Year;

            if (!years.Contains(currentYear)) years.Add(currentYear);

            return years;
        }

        public double TotalDue()
        {
            return Context.Selling?.Sum(s => s.SellingDueAmount) ?? 0;
        }

        public double DailySaleAmount(DateTime? date)
        {
            var saleDate = date ?? DateTime.Now;
            return Context.Selling.Where(s => s.SellingDate.Date == saleDate.Date)?
                  .Sum(s => s.SellingTotalPrice - s.SellingDiscountAmount) ?? 0;
        }


        public ICollection<MonthlyAmount> MonthlyAmounts(int year)
        {
            var months = Context.Selling.Where(e => e.SellingDate.Year == year)
                .GroupBy(e => new
                {
                    number = e.SellingDate.Month

                })
                .Select(g => new MonthlyAmount
                {
                    MonthNumber = g.Key.number,
                    Amount = g.Sum(e => e.SellingTotalPrice - e.SellingDiscountAmount)
                })
                .ToList();

            return months;
        }
    }
}