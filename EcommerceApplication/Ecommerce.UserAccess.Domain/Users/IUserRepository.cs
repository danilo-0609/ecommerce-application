namespace Ecommerce.UserAccess.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> GetByIdAsync(UserId userId);

    Task<User?> GetByEmailAsync(string email);
}
