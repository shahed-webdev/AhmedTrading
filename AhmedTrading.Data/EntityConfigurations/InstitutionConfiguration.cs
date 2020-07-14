using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
    {
        public void Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.Property(e => e.Address).HasMaxLength(500);

            builder.Property(e => e.City).HasMaxLength(128);

            builder.Property(e => e.DialogTitle).HasMaxLength(256);

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.Established).HasMaxLength(50);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.InstitutionName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.LocalArea).HasMaxLength(128);

            builder.Property(e => e.Phone).HasMaxLength(50);

            builder.Property(e => e.PostalCode).HasMaxLength(50);

            builder.Property(e => e.State).HasMaxLength(128);

            builder.Property(e => e.Website).HasMaxLength(50);
        }
    }
}
