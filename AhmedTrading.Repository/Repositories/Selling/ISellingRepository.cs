using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface ISellingRepository : IRepository<Selling>
    {
        Task<int> GetNewSnAsync();
        Task<DbResponse<int>> AddCustomAsync(SellingViewModel model, IUnitOfWork db);
        Task<SellingReceiptViewModel> SellingReceiptAsync(int id, IUnitOfWork db);
        DataResult<SellingRecordViewModel> Records(DataRequest request);
        ICollection<int> Years();
        double TotalDue();
        double DateWiseSale(DateTime? fromDate, DateTime? toDate);
        double DateWiseDue(DateTime? fromDate, DateTime? toDate);
        double DateWiseDiscount(DateTime? fromDate, DateTime? toDate);

        double TotalSale();
        double DailySaleAmount(DateTime? day);
        ICollection<MonthlyAmount> MonthlyAmounts(int year);

        DbResponse ReceiptPaymentIsExist(int id);
        DbResponse DeleteReceipt(int id, IUnitOfWork db);
        DbResponse<CustomerDateWiseSaleSummary> DateWiseSellingSummary(DateTime? fromDate, DateTime? toDate);
        DbResponse<List<SellingProductReportModel>> DateWiseProductSellingSummary(DateTime? fromDate, DateTime? toDate);

    }
}