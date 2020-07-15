using System;

namespace AhmedTrading.Repository
{
    public class SellingPaymentViewModel
    {
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }
}