namespace Ecommerce.Catalog.Domain.Products;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    Task<Product?> GetProductById(ProductId id);

    Task<bool> ExistsProductById(ProductId id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product);

    Task DeleteAsync(ProductId productId, CancellationToken cancellationToken);

    Task<List<Product>?> GetAllProductsBySellerAsync(Guid sellerId, CancellationToken cancellationToken);

    Task<List<Product>?> GetAllProductsByNameAsync(string productName, CancellationToken cancellationToken = default);

    Task<List<Product>?> GetAllProductsByProductType(string productType, CancellationToken cancellationToken = default);
}
