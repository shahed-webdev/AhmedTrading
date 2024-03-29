﻿namespace AhmedTrading.Repository
{
    public class BankAccountViewModel
    {
        public int BankAccountId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
    }

    public class BankAccountCreateModel
    {
        public int BankAccountId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }

    public class BankAccountUpdateModel
    {
        public int BankAccountId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }
}