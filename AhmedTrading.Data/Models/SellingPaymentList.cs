using System;

namespace AhmedTrading.Data
{
    public class SellingPaymentList
    {
        public int SellingPaymentListId { get; set; }
        public int SellingPaymentId { get; set; }
        public int SellingId { get; set; }
        public double SellingPaidAmount { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Selling Selling { get; set; }
        public virtual SellingPayment SellingPayment { get; set; }
    }
}
