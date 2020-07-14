using System;

namespace AhmedTrading.Data
{
    public class Advance
    {
        public int AdvanceId { get; set; }
        public string AdvanceName { get; set; }
        public string AdvanceFor { get; set; }
        public double AdvanceAmount { get; set; }
        public string TimePeriod { get; set; }
        public DateTime AdvanceDate { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
