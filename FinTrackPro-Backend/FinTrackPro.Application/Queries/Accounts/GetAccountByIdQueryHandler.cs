using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Application.DTOs;
using FinTrackPro.Domain.Exceptions;
using MediatR;

namespace FinTrackPro.Application.Queries.Accounts;

public sealed class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(query.AccountId, query.UserId, cancellationToken);

        if (account is null)
            throw new NotFoundException("Account", query.AccountId);

        return AccountDto.FromEntity(account);
    }
}
