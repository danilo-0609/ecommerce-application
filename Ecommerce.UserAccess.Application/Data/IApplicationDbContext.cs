using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using Ecommerce.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.UserAccess.Application.Data;

public interface IApplicationDbContext : IUnitOfWork
{
    DbSet<User> Users { get; set; }

    DbSet<UserRegistration> UserRegistrations { get; set; }
}
