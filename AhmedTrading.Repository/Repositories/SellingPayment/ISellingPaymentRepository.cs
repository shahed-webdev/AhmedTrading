using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface ISellingPaymentRepository : IRepository<SellingPayment>
    {
        Task<int> GetNewSnAsync();
        Task<DbResponse> DuePaySingleAsync(SellingDuePaySingleModel model, IUnitOfWork db);
        Task<DbResponse<int>> DuePayMultipleAsync(SellingDuePayMultipleModel model, IUnitOfWork db);

        Task<PaymentReceiptModel> ReceiptAsync(int id, IUnitOfWork db);
        DataResult<SellingPaymentModel> ReceiptDataTable(DataRequest request);
        DbResponse DeleteReceipt(int id, IUnitOfWork db);
        double DateWiseSalePayment(DateTime? fromDate, DateTime? toDate);
        double DateWiseCashSale(DateTime? fromDate, DateTime? toDate);
    }
}