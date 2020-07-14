using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PurchaseListConfiguration : IEntityTypeConfiguration<PurchaseList>
    {
        public void Configure(EntityTypeBuilder<PurchaseList> builder)
        {
            builder.HasIndex(e => e.ProductId);

            builder.HasIndex(e => e.PurchaseId);

            builder.Property(e => e.PurchasePrice).HasComputedColumnSql("([PurchaseQuantity]*[PurchaseUnitPrice])");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.PurchaseList)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseList_Product");

            builder.HasOne(d => d.Purchase)
                .WithMany(p => p.PurchaseList)
                .HasForeignKey(d => d.PurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseList_Purchase");
        }
    }
}
