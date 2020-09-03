using System;

namespace AhmedTrading.Repository
{
    public class TraderSharingPaymentAddModel
    {
        public int TraderId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsGiven { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class TraderSharingPaymentDetailsModel
    {
        public int TraderSharingPaymentId { get; set; }
        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsGiven { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}