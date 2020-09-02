using System;
using System.Collections.Generic;

namespace AhmedTrading.Repository
{
    public class SellingPaymentViewModel
    {
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }
    public class SellingDuePaySingleModel
    {
        public int SellingId { get; set; }
        public int CustomerId { get; set; }
        public int RegistrationId { get; set; }
        public string PaymentMethod { get; set; }
        public double PaidAmount { get; set; }
        public double SellingDiscountAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }


    public class SellingDuePayMultipleModel
    {
        public SellingDuePayMultipleModel()
        {
            Bills = new HashSet<SellingDuePayMultipleBill>();
        }
        public int CustomerId { get; set; }
        public int RegistrationId { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidDate { get; set; }
        public ICollection<SellingDuePayMultipleBill> Bills { get; set; }
    }

    public class SellingDuePayMultipleBill
    {
        public int SellingId { get; set; }
        public double SellingPaidAmount { get; set; }
    }

    public class PaymentReceiptModel
    {
        public PaymentReceiptModel()
        {
            this.Invoices = new HashSet<PaidInvoiceModel>();
        }
        public ICollection<PaidInvoiceModel> Invoices { get; set; }
        public InstitutionVM InstitutionInfo { get; set; }
        public CustomerReceiptViewModel CustomerInfo { get; set; }
        public int SellingPaymentId { get; set; }
        public DateTime PaidDate { get; set; }
        public int ReceiptSn { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string CollectBy { get; set; }


    }
    public class PaidInvoiceModel
    {
        public int SellingId { get; set; }
        public int SellingSn { get; set; }
        public double SellingAmount { get; set; }
        public double SellingPaidAmount { get; set; }
        public DateTime SellingDate { get; set; }
    }

    public class SellingPaymentModel
    {
        public int SellingPaymentId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime PaidDate { get; set; }
        public int ReceiptSn { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentMethod { get; set; }

    }
}