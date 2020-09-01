using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class SellingConfiguration : IEntityTypeConfiguration<Selling>
    {
        public void Configure(EntityTypeBuilder<Selling> builder)
        {
            builder.HasIndex(e => e.CustomerId);

            builder.HasIndex(e => e.RegistrationId);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.SellingDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.SellingDiscountPercentage).HasComputedColumnSql("(case when [SellingTotalPrice]=(0) then (0) else round(([SellingDiscountAmount]*(100))/[SellingTotalPrice],(2)) end)");

            builder.Property(e => e.SellingDueAmount).HasComputedColumnSql("(round(([SellingTotalPrice]+[SellingReturnAmount]+[TransportationCost])-([SellingDiscountAmount]+[SellingPaidAmount]),(2)))");

            builder.Property(e => e.SellingPaymentStatus)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when (([SellingTotalPrice]+[SellingReturnAmount]+[TransportationCost])-([SellingDiscountAmount]+[SellingPaidAmount]))<=(0) then 'Paid' else 'Due' end)");

            builder.Property(e => e.SellingSn).HasColumnName("SellingSN");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Selling)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Selling_Customer");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.Selling)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Selling_Registration");
        }
    }
}
