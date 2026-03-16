using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Application.DTOs;
using FinTrackPro.Domain.Exceptions;
using MediatR;

namespace FinTrackPro.Application.Commands.Accounts;

public sealed class UpdateAccountNameCommandHandler : IRequestHandler<UpdateAccountNameCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;

    public UpdateAccountNameCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> Handle(UpdateAccountNameCommand command, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, command.UserId, cancellationToken);

        if (account is null)
            throw new NotFoundException("Account", command.AccountId);

        account.UpdateName(command.Name);

        await _accountRepository.SaveChangesAsync(cancellationToken);

        return AccountDto.FromEntity(account);
    }
}
