using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AhmedTrading.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Advance> Advance { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<BankDeposit> BankDeposit { get; set; }
        public virtual DbSet<BankLoan> BankLoan { get; set; }
        public virtual DbSet<BankLoanReturn> BankLoanReturn { get; set; }
        public virtual DbSet<BankWithdrew> BankWithdrew { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerPhone> CustomerPhone { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<PageLink> PageLink { get; set; }
        public virtual DbSet<PageLinkAssign> PageLinkAssign { get; set; }
        public virtual DbSet<PageLinkCategory> PageLinkCategory { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonalLoan> PersonalLoan { get; set; }
        public virtual DbSet<PersonalLoanReturn> PersonalLoanReturn { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductBrand> ProductBrand { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<PurchaseList> PurchaseList { get; set; }
        public virtual DbSet<PurchasePayment> PurchasePayment { get; set; }
        public virtual DbSet<PurchasePaymentList> PurchasePaymentList { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Selling> Selling { get; set; }
        public virtual DbSet<SellingList> SellingList { get; set; }
        public virtual DbSet<SellingPayment> SellingPayment { get; set; }
        public virtual DbSet<SellingPaymentList> SellingPaymentList { get; set; }
        public virtual DbSet<Trader> Trader { get; set; }
        public virtual DbSet<TraderSharing> TraderSharing { get; set; }
        public virtual DbSet<TraderSharingPayment> TraderSharingPayment { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorAdvance> VendorAdvance { get; set; }
        public virtual DbSet<VendorCommission> VendorCommission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdvanceConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
            modelBuilder.ApplyConfiguration(new BankDepositConfiguration());
            modelBuilder.ApplyConfiguration(new BankLoanConfiguration());
            modelBuilder.ApplyConfiguration(new BankLoanReturnConfiguration());
            modelBuilder.ApplyConfiguration(new BankWithdrewConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new InstitutionConfiguration());
            modelBuilder.ApplyConfiguration(new PageLinkConfiguration());
            modelBuilder.ApplyConfiguration(new PageLinkAssignConfiguration());
            modelBuilder.ApplyConfiguration(new PageLinkCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalLoanConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalLoanReturnConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseListConfiguration());
            modelBuilder.ApplyConfiguration(new PurchasePaymentConfiguration());
            modelBuilder.ApplyConfiguration(new PurchasePaymentListConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new SellingConfiguration());
            modelBuilder.ApplyConfiguration(new SellingListConfiguration());
            modelBuilder.ApplyConfiguration(new SellingPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new SellingPaymentListConfiguration());
            modelBuilder.ApplyConfiguration(new TraderConfiguration());
            modelBuilder.ApplyConfiguration(new TraderSharingConfiguration());
            modelBuilder.ApplyConfiguration(new TraderSharingPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new VendorAdvanceConfiguration());
            modelBuilder.ApplyConfiguration(new VendorCommissionConfiguration());

            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedInsitutionData();
            modelBuilder.SeedAdminData();
        }

    }
}