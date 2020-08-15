using AhmedTrading.Data;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IPurchasePaymentRepository : IRepository<PurchasePayment>
    {
        Task<int> GetNewSnAsync();
        Task<DbResponse> DuePaySingleAsync(PurchaseDuePaySingleModel model, IUnitOfWork db);
    }
}