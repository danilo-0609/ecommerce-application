namespace Ecommerce.UserAccess.Infrastructure.Authentication;

internal sealed class JwtOptions
{
    internal string Issuer { get; init; } = "EshopApplicationApp";

    internal string Audience { get; init; } = "EshopApplicationService";

    internal string SecretKey { get; init; } = "SuperSecretEshopApplicationApp";
}
