using System;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public class PersonalLoanAddModel
    {
        public int PersonalLoanId { get; set; }
        public int PersonId { get; set; }
        public int RegistrationId { get; set; }
        public string LoanName { get; set; }
        public double LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
    }

    public class PersonalLoanModel
    {
        public int PersonalLoanId { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string LoanName { get; set; }
        public double LoanAmount { get; set; }
        public double ReturnAmount { get; set; }
        public double RemainingAmount { get; set; }
        public DateTime LoanDate { get; set; }
    }

    public class PersonalLoanReturnModel
    {
        public int PersonalLoanReturnId { get; set; }
        public int PersonalLoanId { get; set; }
        public double ReturnAmount { get; set; }
        public DateTime ReturnDate { get; set; }
    }



    public class PersonLoanDetailsModel
    {
        public PersonLoanDetailsModel()
        {
            Returns = new HashSet<PersonalLoanReturnModel>();
        }
        public PersonalLoanModel LoanInfo { get; set; }
        public ICollection<PersonalLoanReturnModel> Returns { get; set; }
    }
}

