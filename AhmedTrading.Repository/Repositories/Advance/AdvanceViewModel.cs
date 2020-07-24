using System;
using System.ComponentModel.DataAnnotations;

namespace AhmedTrading.Repository
{
    public class AdvanceViewModel
    {
        public int AdvanceId { get; set; }
        public string AdvanceName { get; set; }
        public string AdvanceFor { get; set; }
        public double AdvanceAmount { get; set; }
        public string TimePeriod { get; set; }
        public DateTime AdvanceDate { get; set; }
    }

    public class AdvanceAddModel
    {
        public int AdvanceId { get; set; }
        [Required]
        public string AdvanceName { get; set; }
        public string AdvanceFor { get; set; }
        [Required]
        public double AdvanceAmount { get; set; }
        public string TimePeriod { get; set; }
        public DateTime AdvanceDate { get; set; }
    }
}