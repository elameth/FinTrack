using FinTrackPro.Application.Common.Interfaces;
using FinTrackPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTrackPro.Infrastructure.Persistence;

public sealed class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AccountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Account account, CancellationToken cancellationToken) =>
        await _dbContext.Accounts.AddAsync(account, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);

    public async Task<IReadOnlyList<Account>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) =>
        await _dbContext.Accounts
            .Where(account => account.UserId == userId)
            .ToListAsync(cancellationToken);

    public async Task<Account?> GetByIdAsync(Guid accountId, Guid userId, CancellationToken cancellationToken) =>
        await _dbContext.Accounts
            .FirstOrDefaultAsync(account => account.Id == accountId && account.UserId == userId, cancellationToken);
}
