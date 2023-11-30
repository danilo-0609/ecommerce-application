using ErrorOr;

namespace Ecommerce.BuildingBlocks.Domain;

public interface IBusinessRule
{
    static string? Message { get; }

    bool IsBroken();

    Error Error { get; }
}
