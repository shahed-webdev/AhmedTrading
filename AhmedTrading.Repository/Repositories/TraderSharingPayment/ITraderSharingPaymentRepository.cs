using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;

namespace AhmedTrading.Repository
{
    public interface ITraderSharingPaymentRepository : IRepository<TraderSharingPayment>
    {
        DbResponse Add(TraderSharingPaymentAddModel model);
        DataResult<TraderSharingPaymentDetailsModel> GivenDataTable(DataRequest request);
        DataResult<TraderSharingPaymentDetailsModel> TakenDataTable(DataRequest request);
        DbResponse Delete(int id);
    }
}