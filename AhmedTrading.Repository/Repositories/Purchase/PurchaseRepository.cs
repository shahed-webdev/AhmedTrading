using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetNewSnAsync()
        {

            var sn = 0;
            if (await Context.Purchase.AnyAsync().ConfigureAwait(false))
            {
                sn = await Context.Purchase.MaxAsync(p => p == null ? 0 : p.PurchaseSn).ConfigureAwait(false);
            }

            return sn + 1;
        }

        public async Task<DbResponse<int>> AddCustomAsync(PurchaseViewModel model, IUnitOfWork db)
        {
            var response = new DbResponse<int>();

            var newPurchaseSn = await GetNewSnAsync().ConfigureAwait(false);
            var newPurchasePaymentSn = await db.PurchasePayments.GetNewSnAsync().ConfigureAwait(false);
            var purchase = new Purchase
            {
                RegistrationId = model.RegistrationId,
                VendorId = model.VendorId,
                PurchaseSn = newPurchaseSn,
                PurchaseTotalPrice = model.PurchaseTotalPrice,
                PurchaseDiscountAmount = model.PurchaseDiscountAmount,
                PurchasePaidAmount = model.PurchasePaidAmount,
                MemoNumber = model.MemoNumber,
                PurchaseDate = model.PurchaseDate,
                PurchaseList = model.Products.Select(p => new PurchaseList
                {
                    ProductId = p.ProductId,
                    PurchaseUnitPrice = p.PurchaseUnitPrice,
                    SellingUnitPrice = p.SellingUnitPrice,
                    PurchaseQuantity = p.PurchaseQuantity
                }).ToList(),
                PurchasePaymentList = model.PurchasePaidAmount > 0 ?
                    new List<PurchasePaymentList>
                    {
                        new PurchasePaymentList
                        {
                            PurchasePaidAmount = model.PurchasePaidAmount,
                            PurchasePayment = new PurchasePayment
                            {
                                PurchasePaymentId = 0,
                                RegistrationId = model.RegistrationId,
                                VendorId = model.VendorId,
                                ReceiptSn = newPurchasePaymentSn,
                                PaidAmount = model.PurchasePaidAmount,
                                PaymentMethod = model.PaymentMethod,
                                PaidDate = model.PurchaseDate
                            }
                        }
                    } : null
            };

            await Context.Purchase.AddAsync(purchase).ConfigureAwait(false);

            //Update Product Info
            foreach (var item in model.Products)
            {
                var product = Context.Product.Find(item.ProductId);
                product.SellingUnitPrice = item.SellingUnitPrice;
                product.Stock += item.PurchaseQuantity;

                Context.Product.Update(product);
            }

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);

                response.IsSuccess = true;
                response.Message = "Success";
                response.Data = purchase.PurchaseId;
            }
            catch (DbUpdateException e)
            {
                response.Message = e.Message;
                response.IsSuccess = false;
            }

            return response;
        }

        public Task<PurchaseReceiptViewModel> PurchaseReceiptAsync(int id, IUnitOfWork db)
        {
            var purchaseReceipt = Context.Purchase
                .Include(p => p.Vendor)
                .Include(p => p.Registration)
                .Include(p => p.PurchaseList)
                .ThenInclude(pl => pl.Product)
                .ThenInclude(pd => pd.ProductBrand)
                .Include(p => p.PurchaseList)
                .ThenInclude(pl => pl.Product)
                .Include(p => p.PurchasePaymentList)
                .ThenInclude(p => p.PurchasePayment)
                .Select(p => new PurchaseReceiptViewModel
                {
                    PurchaseSn = p.PurchaseSn,
                    PurchaseId = p.PurchaseId,
                    PurchaseTotalPrice = p.PurchaseTotalPrice,
                    PurchaseDiscountAmount = p.PurchaseDiscountAmount,
                    PurchasePaidAmount = p.PurchasePaidAmount,
                    PurchaseDueAmount = p.PurchaseDueAmount,
                    PurchaseDate = p.PurchaseDate,
                    MemoNumber = p.MemoNumber,
                    Products = p.PurchaseList.Select(pd => new PurchaseProductListViewModel
                    {
                        ProductId = pd.ProductId,
                        ProductBrandId = pd.Product.ProductBrandId,
                        BrandName = pd.Product.ProductBrand.BrandName,
                        ProductName = pd.Product.ProductName,
                        SellingUnitPrice = pd.SellingUnitPrice,
                        PurchaseUnitPrice = pd.PurchaseUnitPrice,
                        PurchaseQuantity = pd.PurchaseQuantity,
                        UnitType = pd.Product.UnitType,
                        PurchasePrice = pd.PurchasePrice
                    }).ToList(),
                    Payments = p.PurchasePaymentList.Select(pp => new PurchasePaymentListViewModel
                    {
                        PaymentMethod = pp.PurchasePayment.PaymentMethod,
                        PaidAmount = pp.PurchasePaidAmount,
                        PaidDate = pp.PurchasePayment.PaidDate
                    }).ToList(),
                    VendorInfo = new VendorViewModel
                    {
                        VendorId = p.Vendor.VendorId,
                        VendorCompanyName = p.Vendor.VendorCompanyName,
                        VendorName = p.Vendor.VendorName,
                        VendorAddress = p.Vendor.VendorAddress,
                        VendorPhone = p.Vendor.VendorPhone,
                        InsertDate = p.Vendor.InsertDate,
                        Balance = p.Vendor.Balance
                    },
                    InstitutionInfo = db.Institutions.FindCustom(),
                    SoildBy = p.Registration.Name
                }).FirstOrDefaultAsync(p => p.PurchaseId == id);

            return purchaseReceipt;
        }

        public DataResult<PurchaseRecordViewModel> Records(DataRequest request)
        {
            var r = Context.Purchase.Include(p => p.Vendor).Select(p => new PurchaseRecordViewModel
            {
                PurchaseId = p.PurchaseId,
                VendorId = p.VendorId,
                VendorCompanyName = p.Vendor.VendorCompanyName,
                PurchaseSn = p.PurchaseSn,
                PurchaseAmount = p.PurchaseTotalPrice,
                PurchasePaidAmount = p.PurchasePaidAmount,
                PurchaseDiscountAmount = p.PurchaseDiscountAmount,
                PurchaseDueAmount = p.PurchaseDueAmount,
                PurchaseDate = p.PurchaseDate,
                MemoNumber = p.MemoNumber
            });
            return r.ToDataResult(request);
        }

        public ICollection<int> Years()
        {
            var years = Context.Purchase
                .GroupBy(e => new
                {
                    e.PurchaseDate.Year
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
            return Context.Purchase?.Sum(p => p.PurchaseDueAmount) ?? 0;
        }
        public double TotalPurchase()
        {
            return Context.Purchase?.Sum(p => p.PurchaseTotalPrice - p.PurchaseDiscountAmount) ?? 0;
        }
        public double DailyPurchaseAmount(DateTime? date)
        {
            var saleDate = date ?? DateTime.Now;
            return Context.Purchase.Where(s => s.PurchaseDate.Date == saleDate.Date)?
                       .Sum(s => s.PurchaseTotalPrice - s.PurchaseDiscountAmount) ?? 0;
        }

        public ICollection<MonthlyAmount> MonthlyAmounts(int year)
        {
            var months = Context.Purchase.Where(e => e.PurchaseDate.Year == year)
                .GroupBy(e => new
                {
                    number = e.PurchaseDate.Month

                })
                .Select(g => new MonthlyAmount
                {
                    MonthNumber = g.Key.number,
                    Amount = g.Sum(e => e.PurchaseTotalPrice - e.PurchaseDiscountAmount)
                })
                .ToList();

            return months;
        }

        public DbResponse DeleteReceipt(int id)
        {
            try
            {
                var purchase = Context.Purchase.Include(p => p.PurchaseList).FirstOrDefault(p => p.PurchaseId == id);
                if (purchase == null) return new DbResponse(false, "No Data Found");
                if (Context.PurchasePaymentList.Any(p => p.PurchaseId == purchase.PurchaseId)) return new DbResponse(false, "Payment Exist");

                foreach (var list in purchase.PurchaseList)
                {
                    var product = Context.Product.Find(list.ProductId);
                    product.Stock -= list.PurchaseQuantity;

                    Context.Product.Update(product);
                }
                Context.Purchase.Remove(purchase);
                Context.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse<PurchaseReceiptViewModel> FindReceipt(int id, IUnitOfWork db)
        {
            try
            {
                var purchaseReceipt = Context.Purchase
                    .Include(p => p.Vendor)
                    .Include(p => p.Registration)
                    .Include(p => p.PurchaseList)
                    .ThenInclude(pl => pl.Product)
                    .ThenInclude(pd => pd.ProductBrand)
                    .Include(p => p.PurchaseList)
                    .ThenInclude(pl => pl.Product)
                    .Include(p => p.PurchasePaymentList)
                    .ThenInclude(p => p.PurchasePayment)
                    .Select(p => new PurchaseReceiptViewModel
                    {
                        PurchaseSn = p.PurchaseSn,
                        PurchaseId = p.PurchaseId,
                        PurchaseTotalPrice = p.PurchaseTotalPrice,
                        PurchaseDiscountAmount = p.PurchaseDiscountAmount,
                        PurchasePaidAmount = p.PurchasePaidAmount,
                        PurchaseDueAmount = p.PurchaseDueAmount,
                        PurchaseDate = p.PurchaseDate,
                        MemoNumber = p.MemoNumber,
                        Products = p.PurchaseList.Select(pd => new PurchaseProductListViewModel
                        {
                            ProductId = pd.ProductId,
                            ProductBrandId = pd.Product.ProductBrandId,
                            BrandName = pd.Product.ProductBrand.BrandName,
                            ProductName = pd.Product.ProductName,
                            SellingUnitPrice = pd.SellingUnitPrice,
                            PurchaseUnitPrice = pd.PurchaseUnitPrice,
                            PurchaseQuantity = pd.PurchaseQuantity,
                            UnitType = pd.Product.UnitType,
                            PurchasePrice = pd.PurchasePrice
                        }).ToList(),
                        Payments = p.PurchasePaymentList.Select(pp => new PurchasePaymentListViewModel
                        {
                            PaymentMethod = pp.PurchasePayment.PaymentMethod,
                            PaidAmount = pp.PurchasePaidAmount,
                            PaidDate = pp.PurchasePayment.PaidDate
                        }).ToList(),
                        VendorInfo = new VendorViewModel
                        {
                            VendorId = p.Vendor.VendorId,
                            VendorCompanyName = p.Vendor.VendorCompanyName,
                            VendorName = p.Vendor.VendorName,
                            VendorAddress = p.Vendor.VendorAddress,
                            VendorPhone = p.Vendor.VendorPhone,
                            InsertDate = p.Vendor.InsertDate,
                            Balance = p.Vendor.Balance
                        },
                        InstitutionInfo = db.Institutions.FindCustom(),
                        SoildBy = p.Registration.Name
                    }).FirstOrDefault(p => p.PurchaseId == id);

                if (purchaseReceipt == null) return new DbResponse<PurchaseReceiptViewModel>(false, "No Data Found");

                return new DbResponse<PurchaseReceiptViewModel>(true, "Success") { Data = purchaseReceipt };
            }
            catch (Exception e)
            {
                return new DbResponse<PurchaseReceiptViewModel>(false, e.Message);
            }
        }

        public async Task<DbResponse> ChangeReceiptAsync(PurchaseReceiptChangeModel model, IUnitOfWork db)
        {
            try
            {
                var purchase = Context.Purchase.Include(p => p.PurchaseList).FirstOrDefault(p => p.PurchaseId == model.PurchaseId);
                if (purchase == null) return new DbResponse(false, "No Data Found");

                var newPurchasePaymentSn = await db.PurchasePayments.GetNewSnAsync();
                purchase.MemoNumber = model.MemoNumber;
                purchase.PurchaseDiscountAmount = model.PurchaseDiscountAmount;
                purchase.PurchasePaidAmount = model.PurchasePaidAmount + model.PaidAmount;
                purchase.PurchaseReturnAmount = model.PurchaseReturnAmount;
                purchase.PurchaseTotalPrice = model.PurchaseTotalPrice;
                foreach (var list in purchase.PurchaseList)
                {
                    var product = Context.Product.Find(list.ProductId);
                    product.Stock -= list.PurchaseQuantity;

                    Context.Product.Update(product);
                }


                purchase.PurchaseList = model.Products.Select(p => new PurchaseList
                {
                    ProductId = p.ProductId,
                    PurchaseUnitPrice = p.PurchaseUnitPrice,
                    SellingUnitPrice = p.SellingUnitPrice,
                    PurchaseQuantity = p.PurchaseQuantity
                }).ToList();

                if (model.PaidAmount > 0)
                {

                    var purchaseList = new PurchasePaymentList
                    {
                        PurchaseId = model.PurchaseId,
                        PurchasePaidAmount = model.PaidAmount,
                        PurchasePayment = new PurchasePayment
                        {
                            PurchasePaymentId = 0,
                            RegistrationId = purchase.RegistrationId,
                            VendorId = purchase.VendorId,
                            ReceiptSn = newPurchasePaymentSn,
                            PaidAmount = model.PaidAmount,
                            PaymentMethod = model.PaymentMethod,
                            PaidDate = model.PaidDate
                        }
                    };
                    Context.PurchasePaymentList.Add(purchaseList);
                }

                Context.Purchase.Update(purchase);
                Context.SaveChanges();

                //Update Product Info
                foreach (var item in model.Products)
                {
                    var product = Context.Product.Find(item.ProductId);
                    product.SellingUnitPrice = item.SellingUnitPrice;
                    product.Stock += item.PurchaseQuantity;

                    Context.Product.Update(product);
                }

                db.Vendors.UpdatePaidDue(purchase.VendorId);
                db.SaveChanges();

                return new DbResponse(true, "Success");
            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }
    }
}