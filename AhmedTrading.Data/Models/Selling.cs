using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Selling
    {
        public Selling()
        {
            SellingList = new HashSet<SellingList>();
            SellingPaymentList = new HashSet<SellingPaymentList>();
        }

        public int SellingId { get; set; }
        public int RegistrationId { get; set; }
        public int? CustomerId { get; set; }
        public int SellingSn { get; set; }
        public double SellingTotalPrice { get; set; }
        public double SellingDiscountAmount { get; set; }
        public double SellingDiscountPercentage { get; set; }
        public double SellingPaidAmount { get; set; }
        public double SellingReturnAmount { get; set; }
        public double SellingDueAmount { get; set; }
        public string SellingPaymentStatus { get; set; }
        public DateTime SellingDate { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual ICollection<SellingList> SellingList { get; set; }
        public virtual ICollection<SellingPaymentList> SellingPaymentList { get; set; }
    }
}
