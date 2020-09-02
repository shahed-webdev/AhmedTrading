using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PersonalLoanConfiguration : IEntityTypeConfiguration<PersonalLoan>
    {
        public void Configure(EntityTypeBuilder<PersonalLoan> builder)
        {
            builder.Property(p => p.LoanName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(p => p.LoanAmount)
                .IsRequired();
            builder.Property(p => p.LoanDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.RemainingAmount)
                .HasComputedColumnSql("([LoanAmount]-[ReturnAmount])");



        }
    }
}