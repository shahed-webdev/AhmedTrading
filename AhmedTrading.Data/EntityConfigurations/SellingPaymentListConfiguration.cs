using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class SellingPaymentListConfiguration : IEntityTypeConfiguration<SellingPaymentList>
    {
        public void Configure(EntityTypeBuilder<SellingPaymentList> builder)
        {
            builder.HasIndex(e => e.SellingId);

            builder.HasIndex(e => e.SellingPaymentId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Selling)
                .WithMany(p => p.SellingPaymentList)
                .HasForeignKey(d => d.SellingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SellingPaymentList_Selling");

            builder.HasOne(d => d.SellingPayment)
                .WithMany(p => p.SellingPaymentList)
                .HasForeignKey(d => d.SellingPaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SellingPaymentList_SellingPayment");
        }
    }
}
