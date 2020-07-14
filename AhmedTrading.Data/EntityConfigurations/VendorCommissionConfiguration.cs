using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class VendorCommissionConfiguration : IEntityTypeConfiguration<VendorCommission>
    {
        public void Configure(EntityTypeBuilder<VendorCommission> builder)
        {
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.MonthDate).HasColumnType("date");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorCommission)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendorCommission_Vendor");
        }
    }
}
