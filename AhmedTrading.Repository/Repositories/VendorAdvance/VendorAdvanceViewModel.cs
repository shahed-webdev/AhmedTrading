using System;

namespace AhmedTrading.Repository
{
    public class VendorAdvanceAddViewModel
    {
        public int VendorId { get; set; }
        public double Advance { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string PaymentMethod { get; set; }
        public string AdvanceDetails { get; set; }
    }
    public class VendorAdvanceRecordViewModel
    {
        public int VendorAdvanceId { get; set; }
        public double Advance { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string PaymentMethod { get; set; }
        public string AdvanceDetails { get; set; }
    }
}