using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Queries.Accounts;

public record GetAccountByIdQuery(Guid UserId, Guid AccountId) : IRequest<AccountDto>;
