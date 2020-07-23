using System;
using System.Collections.Generic;
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


    public class VendorProfileViewModel
    {
        public VendorProfileViewModel()
        {
            Products = new HashSet<ProductViewModel>();
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
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
