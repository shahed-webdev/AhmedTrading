using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Product
    {
        public Product()
        {
            PurchaseList = new HashSet<PurchaseList>();
            SellingList = new HashSet<SellingList>();
            TraderSharing = new HashSet<TraderSharing>();
            VendorCommission = new List<VendorCommission>();
        }

        public int ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public double SellingUnitPrice { get; set; }
        public string UnitType { get; set; }
        public double Stock { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }
        public virtual ICollection<PurchaseList> PurchaseList { get; set; }
        public virtual ICollection<SellingList> SellingList { get; set; }
        public virtual ICollection<TraderSharing> TraderSharing { get; set; }
        public virtual ICollection<VendorCommission> VendorCommission { get; set; }
    }
}
