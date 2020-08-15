using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class BankAccount
    {
        public BankAccount()
        {
            BankDeposit = new HashSet<BankDeposit>();
            BankLoan = new HashSet<BankLoan>();
            BankWithdrew = new HashSet<BankWithdrew>();
        }

        public int BankAccountId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<BankDeposit> BankDeposit { get; set; }
        public virtual ICollection<BankLoan> BankLoan { get; set; }
        public virtual ICollection<BankWithdrew> BankWithdrew { get; set; }
    }
}
