using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class VendorAdvanceConfiguration : IEntityTypeConfiguration<VendorAdvance>
    {
        public void Configure(EntityTypeBuilder<VendorAdvance> builder)
        {
            builder.Property(e => e.AdvanceDate).HasColumnType("date");

            builder.Property(e => e.AdvanceDetails).HasMaxLength(1000);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PaymentMethod).HasMaxLength(50);

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorAdvance)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendorAdvance_Vendor");
        }
    }
}
