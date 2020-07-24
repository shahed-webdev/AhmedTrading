namespace AhmedTrading.Repository
{
    public class DashboardRepository
    {
        private readonly IUnitOfWork _db;

        public DashboardRepository(IUnitOfWork db)
        {
            _db = db;
        }

        public DashboardSummaryViewModel Summary()
        {
            var summary = new DashboardSummaryViewModel
            {
                Sale = _db.Selling.TotalSale(),
                Purchase = _db.Purchases.TotalPurchase(),
                Expense = _db.Expenses.TotalExpense(),
                Withdrew = _db.BankAccounts.TotalWithdrew(),
                Deposit = _db.BankAccounts.TotalDeposit(),
                CustomerDue = _db.Selling.TotalDue(),
                VendorDue = _db.Purchases.TotalDue(),
                Loan = _db.BankLoans.TotalLoan(),
                Advance = _db.Advance.TotalAdvance()
            };
            return summary;
        }
    }
}