namespace AhmedTrading.Repository
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public double SellingUnitPrice { get; set; }
        public string UnitType { get; set; }
        public double Stock { get; set; }
    }

    public class ProductUpdateModel
    {
        public int ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public double SellingUnitPrice { get; set; }
        public string UnitType { get; set; }
        public double Stock { get; set; }
    }

    public class ProductPurchaseViewModel
    {
        public int ProductId { get; set; }
        public double PurchaseQuantity { get; set; }
        public double PurchaseUnitPrice { get; set; }
        public double SellingUnitPrice { get; set; }
    }

    public class ProductSellViewModel
    {
        public int ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public double SellingUnitPrice { get; set; }
    }
}