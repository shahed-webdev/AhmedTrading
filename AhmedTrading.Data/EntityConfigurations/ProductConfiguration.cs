using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.UnitType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.ProductBrand)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ProductBrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductBrand");
        }
    }
}
