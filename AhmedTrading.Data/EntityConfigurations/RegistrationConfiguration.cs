using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.Property(e => e.Address).HasMaxLength(1000);

            builder.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateofBirth).HasMaxLength(50);

            builder.Property(e => e.Designation).HasMaxLength(128);

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.FatherName).HasMaxLength(128);

            builder.Property(e => e.Name).HasMaxLength(128);

            builder.Property(e => e.NationalId)
                .HasColumnName("NationalID")
                .HasMaxLength(128);

            builder.Property(e => e.Phone).HasMaxLength(50);

            builder.Property(e => e.Ps)
                .HasColumnName("PS")
                .HasMaxLength(50);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Validation)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        }
    }
}
