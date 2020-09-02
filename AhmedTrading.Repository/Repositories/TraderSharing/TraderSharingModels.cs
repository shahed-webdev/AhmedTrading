using System;

namespace AhmedTrading.Repository
{
    public class TraderSharingDetailsModel
    {
        public int TraderSharingId { get; set; }
        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SharePrice { get; set; }
        public bool IsGiven { get; set; }
        public DateTime ShareDate { get; set; }
    }

    public class TraderSharingAddModel
    {
        public int TraderId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public bool IsGiven { get; set; }
        public DateTime ShareDate { get; set; }
    }
}