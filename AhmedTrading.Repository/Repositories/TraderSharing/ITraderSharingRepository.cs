using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;

namespace AhmedTrading.Repository
{
    public interface ITraderSharingRepository : IRepository<TraderSharing>
    {
        DbResponse Add(TraderSharingAddModel model);
        DataResult<TraderSharingDetailsModel> GivenDataTable(DataRequest request);
        DataResult<TraderSharingDetailsModel> TakenDataTable(DataRequest request);
        DbResponse Delete(int id);
    }


}