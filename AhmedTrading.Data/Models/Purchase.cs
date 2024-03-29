﻿using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Purchase
    {
        public Purchase()
        {
            PurchaseList = new HashSet<PurchaseList>();
            PurchasePaymentList = new HashSet<PurchasePaymentList>();
        }

        public int PurchaseId { get; set; }
        public int RegistrationId { get; set; }
        public int VendorId { get; set; }
        public int PurchaseSn { get; set; }
        public double PurchaseTotalPrice { get; set; }
        public double PurchaseDiscountAmount { get; set; }
        public double PurchaseDiscountPercentage { get; set; }
        public double PurchasePaidAmount { get; set; }
        public double PurchaseReturnAmount { get; set; }
        public double PurchaseDueAmount { get; set; }
        public string PurchasePaymentStatus { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime InsertDate { get; set; }
        public string MemoNumber { get; set; }

        public virtual Registration Registration { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchaseList> PurchaseList { get; set; }
        public virtual ICollection<PurchasePaymentList> PurchasePaymentList { get; set; }
    }
}
