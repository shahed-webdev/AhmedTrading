﻿using System;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel()
        {
            this.Products = new HashSet<ProductPurchaseViewModel>();
        }

        public int PurchaseId { get; set; }
        public int RegistrationId { get; set; }
        public int VendorId { get; set; }
        public double PurchaseTotalPrice { get; set; }
        public double PurchaseDiscountAmount { get; set; }
        public double PurchasePaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string MemoNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public ICollection<ProductPurchaseViewModel> Products { get; set; }
    }

    public class PurchaseReceiptViewModel
    {
        public PurchaseReceiptViewModel()
        {
            this.Products = new HashSet<PurchaseProductListViewModel>();
            this.Payments = new HashSet<PurchasePaymentListViewModel>();
        }
        public InstitutionVM InstitutionInfo { get; set; }
        public int PurchaseSn { get; set; }
        public int PurchaseId { get; set; }
        public double PurchaseTotalPrice { get; set; }
        public double PurchaseDiscountAmount { get; set; }
        public double PurchasePaidAmount { get; set; }
        public double PurchaseDueAmount { get; set; }
        public string MemoNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public ICollection<PurchaseProductListViewModel> Products { get; set; }
        public ICollection<PurchasePaymentListViewModel> Payments { get; set; }
        public VendorViewModel VendorInfo { get; set; }
        public string SoildBy { get; set; }
    }

    public class PurchaseRecordViewModel
    {
        public int PurchaseId { get; set; }
        public int VendorId { get; set; }
        public string VendorCompanyName { get; set; }
        public int PurchaseSn { get; set; }
        public double PurchaseAmount { get; set; }
        public double PurchasePaidAmount { get; set; }
        public double PurchaseDiscountAmount { get; set; }
        public double PurchaseDueAmount { get; set; }
        public string MemoNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class PurchaseReceiptChangeModel
    {
        public PurchaseReceiptChangeModel()
        {
            Products = new HashSet<ProductPurchaseViewModel>();
        }
        public int PurchaseId { get; set; }
        public double PurchaseTotalPrice { get; set; }
        public double PurchaseDiscountAmount { get; set; }
        public double PurchasePaidAmount { get; set; }
        public double PurchaseDueAmount { get; set; }
        public double PurchaseReturnAmount { get; set; }
        public string MemoNumber { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidDate { get; set; }
        public ICollection<ProductPurchaseViewModel> Products { get; set; }
    }

    public class PurchaseSummary
    {
        public double PurchaseAmount { get; set; }
        public double PaidAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double DueAmount { get; set; }
    }
}