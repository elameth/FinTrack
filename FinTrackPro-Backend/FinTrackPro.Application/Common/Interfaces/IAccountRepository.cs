using FinTrackPro.Domain.Entities;

namespace FinTrackPro.Application.Common.Interfaces;

public interface IAccountRepository
{
    Task AddAsync(Account account, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Account>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Account?> GetByIdAsync(Guid accountId, Guid userId, CancellationToken cancellationToken);
}
