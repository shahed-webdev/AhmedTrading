namespace AhmedTrading.Data
{
    public class SellingList
    {
        public int SellingListId { get; set; }
        public int SellingId { get; set; }
        public int ProductId { get; set; }
        public double SellingQuantity { get; set; }
        public double SellingUnitPrice { get; set; }
        public double SellingPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Selling Selling { get; set; }
    }
}
