using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class SellingPaymentConfiguration : IEntityTypeConfiguration<SellingPayment>
    {
        public void Configure(EntityTypeBuilder<SellingPayment> builder)
        {
            builder.HasIndex(e => e.CustomerId);

            builder.HasIndex(e => e.RegistrationId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PaidDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PaymentMethod).HasMaxLength(50);

            builder.Property(e => e.ReceiptSn).HasColumnName("ReceiptSN");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.SellingPayment)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SellingPayment_Customer");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.SellingPayment)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SellingPayment_Registration");
        }
    }
}
