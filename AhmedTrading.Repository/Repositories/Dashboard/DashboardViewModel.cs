namespace AhmedTrading.Repository
{
    public class DashboardViewModel
    {

    }

    public class DashboardSummaryViewModel
    {
        public double Sale { get; set; }
        public double SalePayment { set; get; }
        public double SaleDue { get; set; }
        public double SaleDiscount { get; set; }
        public double Purchase { get; set; }
        public double PurchasePayment { get; set; }
        public double PurchaseDue { get; set; }
        public double PurchaseDiscount { get; set; }
        public double Expense { get; set; }
        public double Withdrew { get; set; }
        public double Deposit { get; set; }
        public double Loan { get; set; }
        public double Advance { get; set; }
        public double Commission { get; set; }
    }
}