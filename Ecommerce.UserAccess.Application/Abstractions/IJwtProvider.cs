using Ecommerce.UserAccess.Domain.Users;

namespace Ecommerce.UserAccess.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}
