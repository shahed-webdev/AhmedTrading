using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasIndex(e => e.RegistrationId);

            builder.HasIndex(e => e.VendorId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.MemoNumber).HasMaxLength(50);

            builder.Property(e => e.PurchaseDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PurchaseDiscountPercentage).HasComputedColumnSql("(case when [PurchaseTotalPrice]=(0) then (0) else round(([PurchaseDiscountAmount]*(100))/[PurchaseTotalPrice],(2)) end)");

            builder.Property(e => e.PurchaseDueAmount).HasComputedColumnSql("(round(([PurchaseTotalPrice]+[PurchaseReturnAmount])-([PurchaseDiscountAmount]+[PurchasePaidAmount]),(2)))");

            builder.Property(e => e.PurchasePaymentStatus)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when (([PurchaseTotalPrice]+[PurchaseReturnAmount])-([PurchaseDiscountAmount]+[PurchasePaidAmount]))<=(0) then 'Paid' else 'Due' end)");

            builder.Property(e => e.PurchaseSn).HasColumnName("PurchaseSN");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.Purchase)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_Registration");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.Purchase)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_Vendor");
        }
    }
}
