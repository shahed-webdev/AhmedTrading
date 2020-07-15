namespace AhmedTrading.Repository
{
    public class PurchaseProductListViewModel
    {
        public int ProductId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public double SellingUnitPrice { get; set; }
        public double PurchaseQuantity { get; set; }
        public double PurchaseUnitPrice { get; set; }
        public string UnitType { get; set; }
        public double PurchasePrice { get; set; }
    }
}