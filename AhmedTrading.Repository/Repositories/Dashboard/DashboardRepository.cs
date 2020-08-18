using System;

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
                SaleDue = _db.Selling.TotalDue(),
                PurchaseDue = _db.Purchases.TotalDue(),
                Loan = _db.BankLoans.TotalLoan(),
                Advance = _db.Advance.TotalAdvance(),
                Commission = _db.VendorCommissions.TotalCommission()
            };
            return summary;
        }


        public DashboardSummaryViewModel DateWiseSummary(DateTime? fromDate, DateTime? toDate)
        {
            var summary = new DashboardSummaryViewModel
            {
                Sale = _db.Selling.DateWiseSale(fromDate, toDate),
                Purchase = _db.Purchases.DateWisePurchase(fromDate, toDate),
                Expense = _db.Expenses.DateWiseExpense(fromDate, toDate),
                Withdrew = _db.BankAccounts.DateWiseWithdrew(fromDate, toDate),
                Deposit = _db.BankAccounts.DateWiseDeposit(fromDate, toDate),
                SaleDue = _db.Selling.DateWiseSale(fromDate, toDate),
                PurchaseDue = _db.Purchases.DateWisePurchaseDue(fromDate, toDate),
                Loan = _db.BankLoans.DateWiseLoan(fromDate, toDate),
                Advance = _db.Advance.DateWiseAdvance(fromDate, toDate) + _db.VendorAdvances.DateWiseVendorAdvance(fromDate, toDate),
                PurchaseDiscount = _db.Purchases.DateWisePurchaseDiscount(fromDate, toDate),
                PurchasePayment = _db.PurchasePayments.DateWisePurchasePayment(fromDate, toDate),
                SaleDiscount = _db.Selling.DateWiseDiscount(fromDate, toDate),
                SalePayment = _db.SellingPayments.DateWiseSalePayment(fromDate, toDate)
            };
            return summary;
        }
    }
}