using MediatR;

namespace FinTrackPro.Application.Commands.Accounts;

public record DeactivateAccountCommand(
    Guid UserId,
    Guid AccountId) : IRequest;
