using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Trader
    {
        public Trader()
        {
            TraderSharing = new HashSet<TraderSharing>();
        }

        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public string Phone { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<TraderSharing> TraderSharing { get; set; }
    }
}
