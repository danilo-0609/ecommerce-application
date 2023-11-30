using Ecommerce.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.UserAccess.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .Where(d => d.Email == email)
            .SingleOrDefaultAsync();
    }

    public async Task<User?> GetByIdAsync(UserId userId)
    {
        return await _dbContext.Users
            .Where(d => d.UserId == userId)
            .SingleOrDefaultAsync();
    }
}
