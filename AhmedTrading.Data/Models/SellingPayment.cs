﻿using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class SellingPayment
    {
        public SellingPayment()
        {
            SellingPaymentList = new HashSet<SellingPaymentList>();
        }

        public int SellingPaymentId { get; set; }
        public int RegistrationId { get; set; }
        public int? CustomerId { get; set; }
        public int ReceiptSn { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual ICollection<SellingPaymentList> SellingPaymentList { get; set; }
    }
}
