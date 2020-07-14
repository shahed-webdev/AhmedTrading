using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Customer
    {
        public Customer()
        {
            CustomerPhone = new HashSet<CustomerPhone>();
            Selling = new HashSet<Selling>();
            SellingPayment = new HashSet<SellingPayment>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public double TotalAmount { get; set; }
        public double TotalDiscount { get; set; }
        public double ReturnAmount { get; set; }
        public double Paid { get; set; }
        public double Due { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<CustomerPhone> CustomerPhone { get; set; }
        public virtual ICollection<Selling> Selling { get; set; }
        public virtual ICollection<SellingPayment> SellingPayment { get; set; }
    }
}
