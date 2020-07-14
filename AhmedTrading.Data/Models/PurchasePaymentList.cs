using System;

namespace AhmedTrading.Data
{
    public class PurchasePaymentList
    {
        public int PurchasePaymentListId { get; set; }
        public int PurchasePaymentId { get; set; }
        public int PurchaseId { get; set; }
        public double PurchasePaidAmount { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Purchase Purchase { get; set; }
        public virtual PurchasePayment PurchasePayment { get; set; }
    }
}
