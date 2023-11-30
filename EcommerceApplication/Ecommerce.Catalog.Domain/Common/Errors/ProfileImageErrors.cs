using ErrorOr;

namespace Ecommerce.Catalog.Domain.Common.Errors;

public static class ProfileImageErrors
{
    public static Error InvalidFormat(string description) =>
            Error.Validation("Image.InvalidFormat", description);

    public static Error ExcessiveSize(string description) =>
        Error.Validation("Image.ExcessiveSize", description);
}
