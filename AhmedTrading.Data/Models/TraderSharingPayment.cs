using System;

namespace AhmedTrading.Data
{
    public class TraderSharingPayment
    {
        public int TraderSharingPaymentId { get; set; }
        public int TraderId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsGiven { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual Trader Trader { get; set; }
    }
}