﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AhmedTrading.Repository
{
    public class CustomerAddUpdateViewModel
    {
        public CustomerAddUpdateViewModel()
        {
            PhoneNumbers = new HashSet<CustomerPhoneViewModel>();
        }
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public double OpeningDue { get; set; }
        [Required]
        public ICollection<CustomerPhoneViewModel> PhoneNumbers { get; set; }
    }

    public class CustomerPhoneViewModel
    {
        public int? CustomerPhoneId { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool? IsPrimary { get; set; }
    }

    public class CustomerListViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhonePrimary { get; set; }
        public double Due { get; set; }
        public double SaleDue { get; set; }
        public DateTime SignUpDate { get; set; }
    }
    public class CustomerReceiptViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
    }
    public class CustomerProfileViewModel
    {
        public CustomerProfileViewModel()
        {
            PhoneNumbers = new HashSet<CustomerPhoneViewModel>();
            SellingRecords = new HashSet<CustomerSellingViewModel>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime SignUpDate { get; set; }
        public ICollection<CustomerPhoneViewModel> PhoneNumbers { get; set; }
        public ICollection<CustomerSellingViewModel> SellingRecords { get; set; }
        public double SoldAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double DueAmount { get; set; }
        public double SaleDue { get; set; }
        public double OpeningDue { get; set; }
    }
    public class CustomerSellingViewModel
    {
        public int CustomerId { get; set; }
        public int SellingId { get; set; }
        public int SellingSn { get; set; }
        public double SellingAmount { get; set; }
        public double TransportationCost { get; set; }
        public double SellingPaidAmount { get; set; }
        public double SellingDiscountAmount { get; set; }
        public double SellingDueAmount { get; set; }
        public DateTime SellingDate { get; set; }
    }

    public class CustomerDueViewModel
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public double Due { get; set; }
    }

    public class CustomerDateWiseSaleSummary
    {
        public double SoldAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double DueAmount { get; set; }
    }

    public class CustomerMultipleDueCollectionModel
    {
        public CustomerMultipleDueCollectionModel()
        {
            SellingRecords = new HashSet<CustomerSellingViewModel>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public double SaleDue { get; set; }
        public ICollection<CustomerSellingViewModel> SellingRecords { get; set; }
    }

}
