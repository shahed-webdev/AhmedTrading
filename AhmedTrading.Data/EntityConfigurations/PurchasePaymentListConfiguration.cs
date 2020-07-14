using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PurchasePaymentListConfiguration : IEntityTypeConfiguration<PurchasePaymentList>
    {
        public void Configure(EntityTypeBuilder<PurchasePaymentList> builder)
        {
            builder.HasIndex(e => e.PurchaseId);

            builder.HasIndex(e => e.PurchasePaymentId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Purchase)
                .WithMany(p => p.PurchasePaymentList)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchasePaymentList_Purchase");

            builder.HasOne(d => d.PurchasePayment)
                .WithMany(p => p.PurchasePaymentList)
                .HasForeignKey(d => d.PurchasePaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchasePaymentList_PurchasePayment");
        }
    }
}
