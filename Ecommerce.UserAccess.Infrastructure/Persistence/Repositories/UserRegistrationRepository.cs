using Ecommerce.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.UserAccess.Infrastructure.Persistence.Repositories;

internal sealed class UserRegistrationRepository : IUserRegistrationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRegistrationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UserRegistration userRegistration)
    {
        await _dbContext.UserRegistrations.AddAsync(userRegistration);  
    }

    public async Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
    {
        return await _dbContext.UserRegistrations
            .Where(f => f.UserRegistrationId == userRegistrationId)
            .SingleOrDefaultAsync();
    }
}
