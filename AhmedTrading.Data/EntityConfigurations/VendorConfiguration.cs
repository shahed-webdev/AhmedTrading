using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.Property(e => e.OpeningDue).HasDefaultValueSql("0");
            builder.Property(e => e.Balance).HasComputedColumnSql("(((([Paid]+[Advance])+[Commission])+[TotalDiscount])-([TotalAmount]+[ReturnAmount]+[OpeningDue]))");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.VendorAddress).HasMaxLength(500);

            builder.Property(e => e.VendorCompanyName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.VendorName).HasMaxLength(128);

            builder.Property(e => e.VendorPhone).HasMaxLength(50);
        }
    }
}
