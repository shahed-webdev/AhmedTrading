using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;

namespace AhmedTrading.Repository
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        DbResponse CreateAccount(BankAccountCreateModel model);
        DbResponse DeleteAccount(int id);
        DbResponse UpdateAccount(BankAccountUpdateModel model);
        DataResult<BankAccountViewModel> AccountListDataTable(DataRequest request);
        bool IsExistAccount(string name);
        bool IsExistAccount(string name, int updateAccountId);

        DbResponse Deposit(BankDepositModel model);
        DbResponse DeleteDeposit(int id);
        DataResult<BankDepositViewModel> DepositListDataTable(DataRequest request);

        DbResponse Withdrew(BankWithdrewModel model);
        DbResponse DeleteWithdrew(int id);
        DataResult<BankWithdrewViewModel> WithdrewListDataTable(DataRequest request);
    }
}