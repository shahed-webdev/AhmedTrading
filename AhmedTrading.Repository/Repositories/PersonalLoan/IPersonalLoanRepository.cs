using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;

namespace AhmedTrading.Repository
{
    public interface IPersonalLoanRepository : IRepository<PersonalLoan>
    {
        DbResponse Add(PersonalLoanAddModel model);
        DataResult<PersonalLoanModel> ListDataTable(DataRequest request);
        bool IsNameExist(string name, int personId);
        bool IsNameExist(string name, int personId, int updateId);
        DbResponse<PersonLoanDetailsModel> Details(int personLoanId);
        DbResponse Delete(int personLoanId);
        DbResponse DeleteReturn(int personalLoanReturnId);
        DbResponse ReturnAdd(PersonalLoanReturnModel model);
    }
}