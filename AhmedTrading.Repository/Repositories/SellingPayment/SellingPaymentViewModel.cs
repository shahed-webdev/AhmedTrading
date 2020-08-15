using System;

namespace AhmedTrading.Repository
{
    public class SellingPaymentViewModel
    {
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }
    public class SellingDuePaySingleModel
    {
        public int SellingId { get; set; }
        public int CustomerId { get; set; }
        public int RegistrationId { get; set; }
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public double SellingDiscountAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }
}