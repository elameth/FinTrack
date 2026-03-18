using FinTrackPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrackPro.Infrastructure.Persistence.Configurations;

public sealed class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasKey(budget => budget.Id);

        builder.Property(budget => budget.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(budget => budget.CategoryId)
            .IsRequired();

        builder.Property(budget => budget.UserId)
            .IsRequired();

        builder.OwnsOne(budget => budget.Amount, amountBuilder =>
        {
            amountBuilder.Property(money => money.Amount)
                .HasColumnName("Amount")
                .HasPrecision(18, 2)
                .IsRequired();

            amountBuilder.Property(money => money.Currency)
                .HasColumnName("Currency")
                .IsRequired();
        });

        builder.Property(budget => budget.Period)
            .IsRequired();

        builder.Property(budget => budget.StartDate);

        builder.Property(budget => budget.CreatedAt)
            .IsRequired();

        builder.Property(budget => budget.UpdatedAt);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(budget => budget.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(budget => budget.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
