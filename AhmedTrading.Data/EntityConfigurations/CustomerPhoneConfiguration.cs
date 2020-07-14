using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
    {
        public void Configure(EntityTypeBuilder<CustomerPhone> builder)
        {
            builder.HasIndex(e => e.CustomerId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerPhone)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerPhone_Customer");
        }
    }
}
