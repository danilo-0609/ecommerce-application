using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.UserAccess.Domain.Users;

public sealed record UserRole : ValueObject
{
    public static UserRole Customer => new UserRole(nameof(Customer));

    public string Value { get; private set; }   

    public static UserRole Administrador => new UserRole(nameof(Administrador));

    public UserRole(string value)
    {
        Value = value;
    }
}
