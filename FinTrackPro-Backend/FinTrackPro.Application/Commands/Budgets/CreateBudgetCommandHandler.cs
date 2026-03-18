using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Application.DTOs;
using FinTrackPro.Domain.Entities;
using FinTrackPro.Domain.Enums;
using FinTrackPro.Domain.ValueObjects;
using MediatR;

namespace FinTrackPro.Application.Commands.Budgets;

public sealed class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;

    public CreateBudgetCommandHandler(
        IBudgetRepository budgetRepository,
        ITransactionRepository transactionRepository)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<BudgetDto> Handle(CreateBudgetCommand command, CancellationToken cancellationToken)
    {
        var amount = new Money(command.Amount, command.Currency);

        var budget = Budget.Create(
            command.Name,
            command.CategoryId,
            command.UserId,
            amount,
            command.Period,
            command.StartDate);

        await _budgetRepository.AddAsync(budget, cancellationToken);
        await _budgetRepository.SaveChangesAsync(cancellationToken);

        var currentPeriod = budget.GetCurrentPeriodRange();
        var spentAmount = await _transactionRepository.GetSpentAmountAsync(
            command.UserId,
            command.CategoryId,
            currentPeriod.StartDate,
            currentPeriod.EndDate,
            cancellationToken);

        return BudgetDto.FromEntity(budget, spentAmount);
    }
}
