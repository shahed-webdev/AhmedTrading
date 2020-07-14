using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class AdvanceConfiguration : IEntityTypeConfiguration<Advance>
    {
        public void Configure(EntityTypeBuilder<Advance> builder)
        {

            builder.Property(e => e.AdvanceDate).HasColumnType("date");

            builder.Property(e => e.AdvanceFor)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.AdvanceName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TimePeriod).HasMaxLength(50);

        }
    }
}
