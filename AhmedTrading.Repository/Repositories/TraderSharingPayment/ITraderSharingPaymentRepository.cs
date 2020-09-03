using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;

namespace AhmedTrading.Repository
{
    public interface ITraderSharingPaymentRepository : IRepository<TraderSharingPayment>
    {
        DbResponse Add(TraderSharingPaymentAddModel model);
        DataResult<TraderSharingPaymentDetailsModel> ListDataTable(DataRequest request);
        DbResponse Delete(int id);
    }
}