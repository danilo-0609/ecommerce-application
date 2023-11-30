namespace Ecommerce.BuildingBlocks.Application;

public interface IExecutionContextAccessor
{
    Guid UserId { get; }
}
