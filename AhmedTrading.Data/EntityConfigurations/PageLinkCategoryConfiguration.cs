using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PageLinkCategoryConfiguration : IEntityTypeConfiguration<PageLinkCategory>
    {
        public void Configure(EntityTypeBuilder<PageLinkCategory> builder)
        {
            builder.HasKey(e => e.LinkCategoryId);

            builder.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.IconClass).HasMaxLength(128);

            builder.Property(e => e.Sn).HasColumnName("SN");
        }
    }
}
