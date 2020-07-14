using System;

namespace AhmedTrading.Data
{
    public class BankLoanReturn
    {
        public int BankLoanReturnId { get; set; }
        public int BankLoanId { get; set; }
        public double ReturnAmount { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual BankLoan BankLoan { get; set; }
    }
}
