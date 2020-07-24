using AhmedTrading.Data;

namespace AhmedTrading.Repository
{
    public interface IBankLoanRepository : IRepository<BankLoan>
    {
        DbResponse AddLoan(BankLoanModel model);
    }

    public class BankLoanModel
    {
    }
}