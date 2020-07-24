using AhmedTrading.Data;

namespace AhmedTrading.Repository
{
    public class BankLoanRepository : Repository<BankLoan>, IBankLoanRepository
    {
        public BankLoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DbResponse AddLoan(BankLoanModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}