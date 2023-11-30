namespace Ecommerce.BuildingBlocks.Domain;

public interface IUnitOfWorkBase 
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
