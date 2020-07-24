using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;

namespace AhmedTrading.Repository
{
    public interface IAdvanceRepository : IRepository<Advance>
    {
        DbResponse Add(AdvanceAddModel model);
        DataResult<AdvanceViewModel> ListDataTable(DataRequest request);
        bool IsNameExist(string name);
        bool IsNameExist(string name, int UpdateId);
        DbResponse<AdvanceViewModel> Details(int id);
        DbResponse Delete(int id);
    }


}