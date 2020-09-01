using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Person
    {
        public Person()
        {
            PersonalLoan = new HashSet<PersonalLoan>();
        }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<PersonalLoan> PersonalLoan { get; set; }
    }
}