using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class TraderSharingConfiguration : IEntityTypeConfiguration<TraderSharing>
    {
        public void Configure(EntityTypeBuilder<TraderSharing> builder)
        {
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.SharePrice).HasComputedColumnSql("([Quantity]*[UnitPrice])");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.TraderSharing)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TraderSharing_Product");

            builder.HasOne(d => d.Trader)
                .WithMany(p => p.TraderSharing)
                .HasForeignKey(d => d.TraderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TraderSharing_Trader");
        }
    }
}
