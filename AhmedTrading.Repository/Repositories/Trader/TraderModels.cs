namespace AhmedTrading.Repository
{
    public class TraderDetailsModel
    {
        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public string Phone { get; set; }
        public double GivenProductPrice { get; set; }
        public double GivenAmount { get; set; }
        public double TakenProductPrice { get; set; }
        public double TakenAmount { get; set; }
        public double NetAmount { get; set; }
    }

    public class TraderModel
    {
        public int TraderId { get; set; }
        public string TraderName { get; set; }
        public string Phone { get; set; }
    }
}