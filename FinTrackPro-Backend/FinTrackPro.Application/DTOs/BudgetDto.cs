using FinTrackPro.Domain.Entities;
using FinTrackPro.Domain.Enums;

namespace FinTrackPro.Application.DTOs;

public sealed record BudgetDto(
    Guid Id,
    string Name,
    Guid CategoryId,
    Guid UserId,
    decimal Amount,
    Currency Currency,
    BudgetPeriod Period,
    DateTime? StartDate,
    DateTime PeriodStartDate,
    DateTime PeriodEndDate,
    decimal SpentAmount,
    decimal SpentPercentage,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static BudgetDto FromEntity(Budget budget, decimal spentAmount)
    {
        var currentPeriod = budget.GetCurrentPeriodRange();

        var spentPercentage = budget.Amount.Amount > 0
            ? Math.Round(spentAmount / budget.Amount.Amount * 100, 2)
            : 0m;

        return new BudgetDto(
            budget.Id,
            budget.Name,
            budget.CategoryId,
            budget.UserId,
            budget.Amount.Amount,
            budget.Amount.Currency,
            budget.Period,
            budget.StartDate,
            currentPeriod.StartDate,
            currentPeriod.EndDate,
            spentAmount,
            spentPercentage,
            budget.CreatedAt,
            budget.UpdatedAt);
    }
}
