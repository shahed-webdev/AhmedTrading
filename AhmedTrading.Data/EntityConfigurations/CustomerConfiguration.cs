using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.CustomerAddress).HasMaxLength(500);

            builder.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(e => e.OpeningDue).HasDefaultValueSql("0");

            builder.Property(e => e.Due).HasComputedColumnSql("(([OpeningDue]+[TotalAmount]+[ReturnAmount])-([TotalDiscount]+[Paid]))");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
        }
    }
}
