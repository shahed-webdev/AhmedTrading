using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class BankLoanConfiguration : IEntityTypeConfiguration<BankLoan>
    {
        public void Configure(EntityTypeBuilder<BankLoan> builder)
        {
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.LoanDate).HasColumnType("date");

            builder.Property(e => e.LoanDetails).HasMaxLength(1000);

            builder.Property(e => e.LoanName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.RemainingAmount).HasComputedColumnSql("([LoanAmount]-[ReturnAmount])");

            builder.Property(e => e.ReturnPeriod).HasMaxLength(128);

            builder.HasOne(d => d.BankAccount)
                .WithMany(p => p.BankLoan)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankLoan_BankAccount");
        }
    }
}
