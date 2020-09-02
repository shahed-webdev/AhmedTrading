using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface ITraderRepository : IRepository<Trader>
    {
        DbResponse Add(TraderModel model);
        DataResult<TraderDetailsModel> ListDataTable(DataRequest request);
        bool IsPhoneExist(string phone);
        bool IsPhoneExist(string phone, int updateId);
        DbResponse<TraderDetailsModel> Details(int id);
        DbResponse Delete(int id);
        Task<ICollection<TraderModel>> SearchAsync(string key);
    }


}