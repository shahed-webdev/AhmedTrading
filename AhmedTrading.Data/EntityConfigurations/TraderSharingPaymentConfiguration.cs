using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class TraderSharingPaymentConfiguration : IEntityTypeConfiguration<TraderSharingPayment>
    {
        public void Configure(EntityTypeBuilder<TraderSharingPayment> builder)
        {
            builder.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.PaymentMethod)
                .HasMaxLength(50);

            builder.HasOne(d => d.Trader)
                .WithMany(p => p.TraderSharingPayment)
                .HasForeignKey(d => d.TraderId)
                .HasConstraintName("FK_TraderSharingPayment_Trader");
        }
    }
}