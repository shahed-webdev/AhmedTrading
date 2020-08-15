using AhmedTrading.Data;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface ISellingPaymentRepository : IRepository<SellingPayment>
    {
        Task<int> GetNewSnAsync();
        Task<DbResponse> DuePaySingleAsync(SellingDuePaySingleModel model, IUnitOfWork db);
    }
}