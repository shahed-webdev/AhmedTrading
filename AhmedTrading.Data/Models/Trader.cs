using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Trader
    {
        public Trader()
        {
            TraderSharing = new HashSet<TraderSharing>();
            TraderSharingPayment = new HashSet<TraderSharingPayment>();
        }

        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public string Phone { get; set; }
        public double GivenProductPrice { get; set; }
        public double GivenAmount { get; set; }
        public double TakenProductPrice { get; set; }
        public double TakenAmount { get; set; }
        public double NetAmount { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual ICollection<TraderSharing> TraderSharing { get; set; }
        public virtual ICollection<TraderSharingPayment> TraderSharingPayment { get; set; }
    }
}
