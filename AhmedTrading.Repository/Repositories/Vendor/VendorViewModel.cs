using System;
using System.ComponentModel.DataAnnotations;

namespace AhmedTrading.Repository
{

    public class VendorViewModel
    {
        public int VendorId { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string VendorCompanyName { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        [Required]
        public string VendorPhone { get; set; }
        public double Balance { get; set; }
        [Display(Name = "Add Date")]
        public DateTime InsertDate { get; set; }
    }

    public class VendorPaidDue
    {
        public int VendorId { get; set; }
        public string VendorCompanyName { get; set; }
        public double VendorDue { get; set; }
        public double VendorPaid { get; set; }
        public double TotalAmount { get; set; }
        public double TotalDiscount { get; set; }
    }
}
