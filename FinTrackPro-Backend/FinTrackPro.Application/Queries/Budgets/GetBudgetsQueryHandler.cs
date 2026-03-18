using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Queries.Budgets;

public sealed class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, IReadOnlyList<BudgetDto>>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;

    public GetBudgetsQueryHandler(
        IBudgetRepository budgetRepository,
        ITransactionRepository transactionRepository)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<IReadOnlyList<BudgetDto>> Handle(GetBudgetsQuery query, CancellationToken cancellationToken)
    {
        var budgets = await _budgetRepository.GetByUserIdAsync(query.UserId, cancellationToken);
        var result = new List<BudgetDto>(budgets.Count);

        foreach (var budget in budgets)
        {
            var currentPeriod = budget.GetCurrentPeriodRange();
            var spentAmount = await _transactionRepository.GetSpentAmountAsync(
                query.UserId,
                budget.CategoryId,
                currentPeriod.StartDate,
                currentPeriod.EndDate,
                cancellationToken);

            result.Add(BudgetDto.FromEntity(budget, spentAmount));
        }

        return result;
    }
}
