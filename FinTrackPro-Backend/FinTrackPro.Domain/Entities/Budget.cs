using FinTrackPro.Domain.Enums;
using FinTrackPro.Domain.ValueObjects;

namespace FinTrackPro.Domain.Entities;

public sealed class Budget
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid UserId { get; private set; }
    public Money Amount { get; private set; }
    public BudgetPeriod Period { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // For EF Core
    private Budget()
    {
        Name = string.Empty;
        Amount = null!;
    }

    public Budget(
        Guid id,
        string name,
        Guid categoryId,
        Guid userId,
        Money amount,
        BudgetPeriod period,
        DateTime? startDate = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Budget id cannot be empty.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Budget name cannot be empty.", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("Budget name cannot exceed 100 characters.", nameof(name));

        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category id cannot be empty.", nameof(categoryId));

        if (userId == Guid.Empty)
            throw new ArgumentException("User id cannot be empty.", nameof(userId));

        if (amount.Amount <= 0)
            throw new ArgumentException("Budget amount must be greater than zero.", nameof(amount));

        Id = id;
        Name = name.Trim();
        CategoryId = categoryId;
        UserId = userId;
        Amount = amount;
        Period = period;
        StartDate = startDate.HasValue
            ? DateTime.SpecifyKind(startDate.Value.Date, DateTimeKind.Utc)
            : null;
        CreatedAt = DateTime.UtcNow;
    }

    public static Budget Create(
        string name,
        Guid categoryId,
        Guid userId,
        Money amount,
        BudgetPeriod period,
        DateTime? startDate = null)
    {
        return new Budget(Guid.NewGuid(), name, categoryId, userId, amount, period, startDate);
    }

    public DateRange GetCurrentPeriodRange()
    {
        var now = DateTime.UtcNow.Date;
        return GetPeriodRangeForDate(now);
    }

    public DateRange GetPeriodRangeForDate(DateTime date)
    {
        var utcDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);

        return Period switch
        {
            BudgetPeriod.Weekly => GetWeekRange(utcDate),
            BudgetPeriod.Monthly => GetMonthRange(utcDate),
            BudgetPeriod.Yearly => GetYearRange(utcDate),
            _ => throw new InvalidOperationException($"Unknown budget period: {Period}")
        };
    }

    private static DateRange GetWeekRange(DateTime date)
    {
        var startOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
        if (startOfWeek > date) startOfWeek = startOfWeek.AddDays(-7);
        var endOfWeek = startOfWeek.AddDays(6);
        return new DateRange(startOfWeek, endOfWeek);
    }

    private static DateRange GetMonthRange(DateTime date)
    {
        var startOfMonth = new DateTime(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
        return new DateRange(startOfMonth, endOfMonth);
    }

    private static DateRange GetYearRange(DateTime date)
    {
        var startOfYear = new DateTime(date.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var endOfYear = new DateTime(date.Year, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        return new DateRange(startOfYear, endOfYear);
    }
}
