using System;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IPageLinkRepository PageLinks { get; }
        IPageLinkCategoryRepository PageLinkCategories { get; }
        IPageLinkAssignRepository PageLinkAssigns { get; }
        IProductRepository Products { get; }
        IProductBrandRepository ProductBrands { get; }
        IPurchaseRepository Purchases { get; }
        IPurchasePaymentRepository PurchasePayments { get; }
        IRegistrationRepository Registrations { get; }
        IExpenseCategoryRepository ExpenseCategories { get; }
        IExpenseRepository Expenses { get; }
        IInstitutionRepository Institutions { get; }
        IVendorRepository Vendors { get; }
        IVendorAdvanceRepository VendorAdvances { get; }
        IVendorCommissionRepository VendorCommissions { get; }
        ISellingRepository Selling { get; }
        ISellingPaymentRepository SellingPayments { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
