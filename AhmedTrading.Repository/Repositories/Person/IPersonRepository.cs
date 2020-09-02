using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        DbResponse Add(PersonModel model);
        DataResult<PersonModel> ListDataTable(DataRequest request);
        bool IsPhoneExist(string phone);
        bool IsPhoneExist(string phone, int updateId);
        DbResponse<PersonModel> Details(int id);
        DbResponse Delete(int id);
        Task<ICollection<PersonModel>> SearchAsync(string key);
    }
}