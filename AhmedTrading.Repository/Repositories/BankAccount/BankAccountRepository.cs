using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;

namespace AhmedTrading.Repository
{
    public class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(ApplicationDbContext context) : base(context)
        {

        }

        public DbResponse CreateAccount(BankAccountCreateModel model)
        {

            var response = new DbResponse();

            try
            {

            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
            throw new System.NotImplementedException();
        }

        public DbResponse DeleteAccount(int id)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse UpdateAccount(BankAccountUpdateModel model)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<BankAccountViewModel> AccountListDataTable(DataRequest request)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistAccount(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistAccount(string name, int updateAccountId)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse Deposit(BankDepositModel model)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse DeleteDeposit(int id)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<BankDepositViewModel> DepositListDataTable(DataRequest request)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse Withdrew(BankWithdrewModel model)
        {
            throw new System.NotImplementedException();
        }

        public DbResponse DeleteWithdrew(int id)
        {
            throw new System.NotImplementedException();
        }

        public DataResult<BankWithdrewViewModel> WithdrewListDataTable(DataRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}