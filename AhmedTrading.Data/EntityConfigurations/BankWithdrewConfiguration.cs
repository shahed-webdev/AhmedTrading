using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class BankWithdrewConfiguration : IEntityTypeConfiguration<BankWithdrew>
    {
        public void Configure(EntityTypeBuilder<BankWithdrew> builder)
        {
            builder.Property(e => e.ActivityDate).HasColumnType("date");

            builder.Property(e => e.Details).HasMaxLength(1000);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.BankAccount)
                .WithMany(p => p.BankWithdrew)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BankWithdrew_BankAccount");
        }
    }
}
