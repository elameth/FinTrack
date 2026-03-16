using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Domain.Exceptions;
using MediatR;

namespace FinTrackPro.Application.Commands.Accounts;

public sealed class DeactivateAccountCommandHandler : IRequestHandler<DeactivateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public DeactivateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task Handle(DeactivateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, command.UserId, cancellationToken);

        if (account is null)
            throw new NotFoundException("Account", command.AccountId);

        account.Deactivate();

        await _accountRepository.SaveChangesAsync(cancellationToken);
    }
}
