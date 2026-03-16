using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Queries.Accounts;

public record GetAccountsQuery(Guid UserId) : IRequest<IReadOnlyList<AccountDto>>;
