using System;

namespace AhmedTrading.Repository
{
    public class BankDepositViewModel
    {
        public int BankDepositId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public DateTime ActivityDate { get; set; }
    }
    public class BankDepositModel
    {
        public int BankDepositId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}