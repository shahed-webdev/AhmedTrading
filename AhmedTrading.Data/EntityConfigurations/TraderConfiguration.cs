using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class TraderConfiguration : IEntityTypeConfiguration<Trader>
    {
        public void Configure(EntityTypeBuilder<Trader> builder)
        {

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.TraderName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.NetAmount)
                .HasComputedColumnSql("(([TakenAmount]+[TakenProductPrice])-([GivenAmount]+[GivenProductPrice]))");

        }
    }
}
