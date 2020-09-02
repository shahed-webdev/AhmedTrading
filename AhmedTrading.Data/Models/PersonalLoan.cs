using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class PersonalLoan
    {
        public PersonalLoan()
        {
            PersonalLoanReturn = new HashSet<PersonalLoanReturn>();
        }
        public int PersonalLoanId { get; set; }
        public int PersonId { get; set; }
        public int RegistrationId { get; set; }
        public string LoanName { get; set; }
        public double LoanAmount { get; set; }
        public double ReturnAmount { get; set; }
        public double RemainingAmount { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual Person Person { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual ICollection<PersonalLoanReturn> PersonalLoanReturn { get; set; }

    }
}