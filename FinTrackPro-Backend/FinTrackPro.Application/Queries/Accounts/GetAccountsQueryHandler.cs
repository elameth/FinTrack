using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Application.DTOs;
using MediatR;

namespace FinTrackPro.Application.Queries.Accounts;

public sealed class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IReadOnlyList<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountsQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IReadOnlyList<AccountDto>> Handle(GetAccountsQuery query, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetByUserIdAsync(query.UserId, cancellationToken);
        return accounts.Select(AccountDto.FromEntity).ToList();
    }
}
