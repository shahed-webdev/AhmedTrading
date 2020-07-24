using AhmedTrading.Data;
using System.Threading.Tasks;

namespace AhmedTrading.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            BankAccounts = new BankAccountRepository(_context);
            BankLoans = new BankLoanRepository(_context);
            Customers = new CustomerRepository(_context);
            PageLinks = new PageLinkRepository(_context);
            PageLinkCategories = new PageLinkCategoryRepository(_context);
            PageLinkAssigns = new PageLinkAssignRepository(_context);
            Products = new ProductRepository(_context);
            ProductBrands = new ProductBrandRepository(_context);
            Purchases = new PurchaseRepository(_context);
            PurchasePayments = new PurchasePaymentRepository(_context);
            Registrations = new RegistrationRepository(_context);
            ExpenseCategories = new ExpenseCategoryRepository(_context);
            Expenses = new ExpenseRepository(_context);
            Institutions = new InstitutionRepository(_context);
            Selling = new SellingRepository(_context);
            SellingPayments = new SellingPaymentRepository(_context);
            Vendors = new VendorRepository(_context);
            VendorAdvances = new VendorAdvanceRepository(_context);
            VendorCommissions = new VendorCommissionRepository(_context);
        }


        public IBankAccountRepository BankAccounts { get; }
        public IBankLoanRepository BankLoans { get; }
        public ICustomerRepository Customers { get; }
        public IPageLinkRepository PageLinks { get; private set; }
        public IPageLinkCategoryRepository PageLinkCategories { get; private set; }
        public IPageLinkAssignRepository PageLinkAssigns { get; private set; }
        public IProductRepository Products { get; }
        public IProductBrandRepository ProductBrands { get; }
        public IPurchaseRepository Purchases { get; }
        public IPurchasePaymentRepository PurchasePayments { get; }
        public IRegistrationRepository Registrations { get; private set; }
        public IExpenseCategoryRepository ExpenseCategories { get; private set; }
        public IExpenseRepository Expenses { get; }
        public IInstitutionRepository Institutions { get; }
        public IVendorRepository Vendors { get; }
        public IVendorAdvanceRepository VendorAdvances { get; }
        public IVendorCommissionRepository VendorCommissions { get; }
        public ISellingRepository Selling { get; }
        public ISellingPaymentRepository SellingPayments { get; }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
