using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Vendor
    {
        public Vendor()
        {
            Purchase = new HashSet<Purchase>();
            PurchasePayment = new HashSet<PurchasePayment>();
            VendorAdvance = new HashSet<VendorAdvance>();
            VendorCommission = new HashSet<VendorCommission>();
        }

        public int VendorId { get; set; }
        public string VendorCompanyName { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
        public double TotalAmount { get; set; }
        public double TotalDiscount { get; set; }
        public double ReturnAmount { get; set; }
        public double Paid { get; set; }
        public double Advance { get; set; }
        public double Commission { get; set; }
        public double Balance { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
        public virtual ICollection<PurchasePayment> PurchasePayment { get; set; }
        public virtual ICollection<VendorAdvance> VendorAdvance { get; set; }
        public virtual ICollection<VendorCommission> VendorCommission { get; set; }
    }
}
