using System;

namespace AhmedTrading.Data
{
    public class BankDeposit
    {
        public int BankDepositId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ActivityDate { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}
