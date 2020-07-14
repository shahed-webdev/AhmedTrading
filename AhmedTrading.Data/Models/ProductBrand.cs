using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class ProductBrand
    {
        public ProductBrand()
        {
            Product = new HashSet<Product>();
        }

        public int ProductBrandId { get; set; }
        public string BrandName { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
