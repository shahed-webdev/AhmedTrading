using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public partial class SellingListConfiguration : IEntityTypeConfiguration<SellingList>
    {
        public void Configure(EntityTypeBuilder<SellingList> builder)
        {
            builder.HasIndex(e => e.ProductId);

            builder.HasIndex(e => e.SellingId);

            builder.Property(e => e.SellingPrice).HasComputedColumnSql("([SellingQuantity]*[SellingUnitPrice])");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.SellingList)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_SellingList_Product");

            builder.HasOne(d => d.Selling)
                .WithMany(p => p.SellingList)
                .HasForeignKey(d => d.SellingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SellingList_Selling");
        }
    }
}
