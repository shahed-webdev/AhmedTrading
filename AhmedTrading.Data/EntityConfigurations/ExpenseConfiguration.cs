using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhmedTrading.Data
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasIndex(e => e.ExpenseCategoryId);

            builder.HasIndex(e => e.RegistrationId);

            builder.Property(e => e.ExpenseDate).HasColumnType("date");

            builder.Property(e => e.ExpenseFor).HasMaxLength(256);

            builder.Property(e => e.ExpensePaymentMethod).HasMaxLength(50);

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.ExpenseCategory)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_ExpenseCategory");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_Registration");
        }
    }
}
