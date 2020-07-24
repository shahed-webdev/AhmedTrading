using System;

namespace AhmedTrading.Repository
{
    public class BankWithdrewViewModel
    {
        public int BankWithdrewId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public DateTime ActivityDate { get; set; }
    }

    public class BankWithdrewModel
    {
        public int BankWithdrewId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}