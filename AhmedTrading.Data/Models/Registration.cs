﻿using System;
using System.Collections.Generic;

namespace AhmedTrading.Data
{
    public class Registration
    {
        public Registration()
        {
            Expense = new HashSet<Expense>();
            PageLinkAssign = new HashSet<PageLinkAssign>();
            Purchase = new HashSet<Purchase>();
            PurchasePayment = new HashSet<PurchasePayment>();
            Selling = new HashSet<Selling>();
            SellingPayment = new HashSet<SellingPayment>();
            PersonalLoan = new HashSet<PersonalLoan>();
        }

        public int RegistrationId { get; set; }
        public string UserName { get; set; }
        public bool? Validation { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Designation { get; set; }
        public string DateofBirth { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreateDate { get; set; }
        public string Ps { get; set; }

        public virtual ICollection<Expense> Expense { get; set; }
        public virtual ICollection<PageLinkAssign> PageLinkAssign { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
        public virtual ICollection<PurchasePayment> PurchasePayment { get; set; }
        public virtual ICollection<Selling> Selling { get; set; }
        public virtual ICollection<SellingPayment> SellingPayment { get; set; }
        public virtual ICollection<PersonalLoan> PersonalLoan { get; set; }


    }
}
