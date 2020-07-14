using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class BankDepositConfiguration : IEntityTypeConfiguration<BankDeposit>
    {
        public void Configure(EntityTypeBuilder<BankDeposit> builder)
        {
            builder.Property(e => e.ActivityDate).HasColumnType("date");

            builder.Property(e => e.Details).HasMaxLength(1000);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.BankAccount)
                .WithMany(p => p.BankDeposit)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankDeposit_BankAccount");
        }
    }
}
