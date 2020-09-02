namespace AhmedTrading.Repository
{
    public class PersonModel
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class PersonDetailsModel
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double LoanAmount { get; set; }
        public double ReturnAmount { get; set; }
        public double RemainingAmount { get; set; }
    }
}