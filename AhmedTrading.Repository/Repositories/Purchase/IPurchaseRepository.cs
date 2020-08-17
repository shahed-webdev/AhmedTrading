using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<int> GetNewSnAsync();
        Task<DbResponse<int>> AddCustomAsync(PurchaseViewModel model, IUnitOfWork db);
        Task<PurchaseReceiptViewModel> PurchaseReceiptAsync(int id, IUnitOfWork db);
        DataResult<PurchaseRecordViewModel> Records(DataRequest request);
        ICollection<int> Years();
        double TotalDue();
        double TotalPurchase();
        double DailyPurchaseAmount(DateTime? date);
        ICollection<MonthlyAmount> MonthlyAmounts(int year);


        DbResponse ReceiptPaymentIsExist(int id);
        DbResponse DeleteReceipt(int id);
        DbResponse<PurchaseReceiptViewModel> FindReceipt(int id, IUnitOfWork db);
        Task<DbResponse> ChangeReceiptAsync(PurchaseReceiptChangeModel model, IUnitOfWork db);
        DbResponse<CustomerDateWiseSaleSummary> SaleSummaryDateWise(DateTime? fromDate, DateTime? toDate);
    }



}