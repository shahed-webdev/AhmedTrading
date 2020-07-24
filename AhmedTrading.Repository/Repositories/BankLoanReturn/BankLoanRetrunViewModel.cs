using System;

namespace AhmedTrading.Repository
{
    public class BankLoanReturnViewModel
    {
        public int BankLoanReturnId { get; set; }
        public int BankLoanId { get; set; }
        public double ReturnAmount { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public class BankLoanReturnModel
    {
        public int BankLoanReturnId { get; set; }
        public int BankLoanId { get; set; }
        public double ReturnAmount { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}