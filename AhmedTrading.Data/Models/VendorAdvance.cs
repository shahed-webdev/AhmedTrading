using System;

namespace AhmedTrading.Data
{
    public class VendorAdvance
    {
        public int VendorAdvanceId { get; set; }
        public int VendorId { get; set; }
        public double Advance { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string PaymentMethod { get; set; }
        public string AdvanceDetails { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
