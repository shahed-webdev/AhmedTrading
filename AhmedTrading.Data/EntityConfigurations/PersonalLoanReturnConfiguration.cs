using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class PersonalLoanReturnConfiguration : IEntityTypeConfiguration<PersonalLoanReturn>

    {
        public void Configure(EntityTypeBuilder<PersonalLoanReturn> builder)
        {
            builder.Property(p => p.ReturnDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.HasOne(r => r.PersonalLoan)
                .WithMany(p => p.PersonalLoanReturn)
                .HasForeignKey(r => r.PersonalLoanId)
                .HasConstraintName("FK_PersonalLoanReturn_PersonalLoan");
        }
    }
}