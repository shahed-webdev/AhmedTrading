using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class BankLoan
    {
        public BankLoan()
        {
            BankLoanReturn = new HashSet<BankLoanReturn>();
        }

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
        public DateTime InsertDate { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual ICollection<BankLoanReturn> BankLoanReturn { get; set; }
    }
}
