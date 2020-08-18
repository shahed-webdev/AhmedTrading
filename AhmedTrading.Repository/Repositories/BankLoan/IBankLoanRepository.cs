using AhmedTrading.Data;
using JqueryDataTables.LoopsIT;
using System;

namespace AhmedTrading.Repository
{
    public interface IBankLoanRepository : IRepository<BankLoan>
    {
        DbResponse AddLoan(BankLoanModel model);
        bool IsLoanNameExist(string name);
        bool IsLoanNameExist(string name, int updateLoanId);
        DbResponse<BankLoanViewModel> FindLoan(int id);
        DataResult<BankLoanViewModel> RecordsDataTable(DataRequest request);
        DataResult<BankLoanReturnViewModel> ReturnRecordsDataTable(DataRequest request);
        DbResponse DeleteLoan(int id);
        DbResponse LoanReturn(BankLoanReturnModel model);
        DbResponse DeleteLoanReturn(int id);
        double TotalLoan();
        double DateWiseLoan(DateTime? fromDate, DateTime? toDate);
    }
}