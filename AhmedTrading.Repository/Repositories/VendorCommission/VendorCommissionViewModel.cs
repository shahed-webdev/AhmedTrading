using System;

namespace AhmedTrading.Repository
{
    public class VendorCommissionViewModel
    {
        public string MonthName { get; set; }
        public string ProductName { get; set; }
        public double Commission { get; set; }
        public int MonthNumber { get; set; }
        public int Year { get; set; }
    }

    public class VendorCommissionAddModel
    {
        public int VendorId { get; set; }
        public int ProductId { get; set; }
        public double Commission { get; set; }
        public DateTime MonthDate { get; set; }
    }
}