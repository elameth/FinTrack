using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Commands.Accounts;

public record UpdateAccountNameCommand(
    Guid UserId,
    Guid AccountId,
    string Name) : IRequest<AccountDto>;
