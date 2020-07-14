using System;

namespace AhmedTrading.Data
{
    public class TraderSharing
    {
        public int TraderSharingId { get; set; }
        public int TraderId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SharePrice { get; set; }
        public bool IsGiven { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Trader Trader { get; set; }
    }
}
