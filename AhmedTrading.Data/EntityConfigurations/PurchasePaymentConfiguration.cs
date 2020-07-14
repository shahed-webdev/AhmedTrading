using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PurchasePaymentConfiguration : IEntityTypeConfiguration<PurchasePayment>
    {
        public void Configure(EntityTypeBuilder<PurchasePayment> builder)
        {
            builder.HasIndex(e => e.RegistrationId);

            builder.HasIndex(e => e.VendorId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PaidDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PaymentMethod).HasMaxLength(50);

            builder.Property(e => e.ReceiptSn).HasColumnName("ReceiptSN");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.PurchasePayment)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchasePayment_Registration");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.PurchasePayment)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchasePayment_Vendor");
        }
    }
}
