using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class BankLoanReturnConfiguration : IEntityTypeConfiguration<BankLoanReturn>
    {
        public void Configure(EntityTypeBuilder<BankLoanReturn> builder)
        {
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ReturnDate).HasColumnType("date");

            builder.HasOne(d => d.BankLoan)
                .WithMany(p => p.BankLoanReturn)
                .HasForeignKey(d => d.BankLoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankLoanReturn_BankLoan");
        }
    }
}
