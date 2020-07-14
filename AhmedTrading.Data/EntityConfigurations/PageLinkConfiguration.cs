using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PageLinkConfiguration : IEntityTypeConfiguration<PageLink>
    {
        public void Configure(EntityTypeBuilder<PageLink> builder)
        {
            builder.HasKey(e => e.LinkId);

            builder.HasIndex(e => e.LinkCategoryId);

            builder.Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Controller)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.IconClass).HasMaxLength(128);

            builder.Property(e => e.RoleId).HasMaxLength(128);

            builder.Property(e => e.Sn).HasColumnName("SN");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasOne(d => d.LinkCategory)
                .WithMany(p => p.PageLink)
                .HasForeignKey(d => d.LinkCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PageLink_PageLinkCategory");
        }
    }
}
