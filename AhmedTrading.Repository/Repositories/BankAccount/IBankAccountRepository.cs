using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        DbResponse CreateAccount(BankAccountCreateModel model);
        DbResponse DeleteAccount(int id);
        DbResponse UpdateAccount(BankAccountUpdateModel model);
        BankAccountViewModel AccountDetails(int id);
        DataResult<BankAccountViewModel> AccountListDataTable(DataRequest request);
        ICollection<DDL> Ddl();
        bool IsExistAccount(string name, string number);
        bool IsExistAccount(string name, string number, int updateAccountId);

        DbResponse Deposit(BankDepositModel model);
        DbResponse DeleteDeposit(int id);
        DataResult<BankDepositViewModel> DepositListDataTable(DataRequest request);

        DbResponse Withdrew(BankWithdrewModel model);
        DbResponse DeleteWithdrew(int id);
        DataResult<BankWithdrewViewModel> WithdrewListDataTable(DataRequest request);
        double TotalDeposit();
        double TotalWithdrew();
    }
}