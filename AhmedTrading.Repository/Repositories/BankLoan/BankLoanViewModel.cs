using System;

namespace AhmedTrading.Repository
{
    public class BankLoanViewModel
    {
        public int BankLoanId { get; set; }
        public int BankAccountId { get; set; }
        public string LoanName { get; set; }
        public double LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
        public double ReturnAmount { get; set; }
        public double RemainingAmount { get; set; }
        public double InterestPercentage { get; set; }
        public string ReturnPeriod { get; set; }
        public string LoanDetails { get; set; }
    }

    public class BankLoanModel
    {
        public int BankLoanId { get; set; }
        public int BankAccountId { get; set; }
        public string LoanName { get; set; }
        public double LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
        public double InterestPercentage { get; set; }
        public string ReturnPeriod { get; set; }
        public string LoanDetails { get; set; }
    }
}