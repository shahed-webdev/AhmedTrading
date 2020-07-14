using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PageLinkAssignConfiguration : IEntityTypeConfiguration<PageLinkAssign>
    {
        public void Configure(EntityTypeBuilder<PageLinkAssign> builder)
        {
            builder.HasKey(e => e.LinkAssignId);

            builder.HasIndex(e => e.LinkId);

            builder.HasIndex(e => e.RegistrationId);

            builder.HasOne(d => d.Link)
                .WithMany(p => p.PageLinkAssign)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PageLinkAssign_PageLink");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.PageLinkAssign)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PageLinkAssign_Registration");
        }
    }
}
