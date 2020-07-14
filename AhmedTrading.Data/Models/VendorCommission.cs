using System;

namespace AhmedTrading.Data
{
    public class VendorCommission
    {
        public int VendorCommissionId { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public double Commission { get; set; }
        public DateTime MonthDate { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
