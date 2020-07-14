namespace AhmedTrading.Data
{
    public class PurchaseList
    {
        public int PurchaseListId { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public double PurchaseQuantity { get; set; }
        public double PurchaseUnitPrice { get; set; }
        public double SellingUnitPrice { get; set; }
        public double PurchasePrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
