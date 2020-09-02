using System;

namespace AhmedTrading.Data
{
    public class PersonalLoanReturn
    {
        public int PersonalLoanReturnId { get; set; }
        public int PersonalLoanId { get; set; }
        public double ReturnAmount { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual PersonalLoan PersonalLoan { get; set; }
    }
}