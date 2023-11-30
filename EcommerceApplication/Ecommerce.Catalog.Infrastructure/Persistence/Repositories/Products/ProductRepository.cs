    using Ecommerce.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories.Products;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
    }

    public async Task DeleteAsync(ProductId productId, CancellationToken cancellationToken)
    {
        await _dbContext.Products
            .Where(x => x.ProductId == productId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> ExistsProductById(ProductId id, CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Products.AnyAsync(x => x.ProductId == id))
        {
            return true;
        }

        return false;
    }

    public async Task<List<Product>?> GetAllProductsByNameAsync(string productName, CancellationToken cancellationToken = default)
    {
        var products = await _dbContext
            .Products
            .Where(d => d.Name == productName)
            .ToListAsync();

        return products;
    }

    public async Task<List<Product>?> GetAllProductsByProductType(string productType, CancellationToken cancellationToken = default)
    {
        var products = await _dbContext
            .Products
            .Where(d => d.ProductType.Value == productType)
            .ToListAsync();

        return products;
    }

    public async Task<List<Product>?> GetAllProductsBySellerAsync(Guid sellerId, CancellationToken cancellationToken)
    {
        var sellers = await _dbContext
            .Products
            .Where(x => x.SellerId == sellerId)
            .ToListAsync();

        List<Product>? products = new();

        foreach (var seller in sellers)
        {
            var product = await _dbContext
                .Products
                .Where(x => x.SellerId == seller.SellerId)
                .SingleOrDefaultAsync();

            products.Add(product!);
        }

        return products;
    }

    public async Task<Product?> GetProductById(ProductId id)
    {
        var product = await _dbContext
            .Products
            .Where(x => x.ProductId == id)
            .SingleOrDefaultAsync();

        if (product is null)
        {
            return null;
        }

        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        await _dbContext
        .Products
        .Where(x => x.ProductId == product.ProductId)
        .ExecuteUpdateAsync(setters =>
                setters.SetProperty(b => b.ProductId, product.ProductId)
                        .SetProperty(b => b.SellerId, product.SellerId)
                        .SetProperty(b => b.Name, product.Name)
                        .SetProperty(b => b.Price.Value, product.Price.Value)
                        .SetProperty(b => b.Description, product.Description)
                        .SetProperty(b => b.Size, product.Size)
                        .SetProperty(b => b.Color, product.Color)
                        .SetProperty(b => b.ProductType.Value, product.ProductType.Value)
                        .SetProperty(b => b.InStock, product.InStock)
                        .SetProperty(b => b.IsActive, product.IsActive)
                        .SetProperty(b => b.CreatedDateTime, product.CreatedDateTime)
                        .SetProperty(b => b.UpdatedDateTime, product.CreatedDateTime)
                        .SetProperty(b => b.ExpireDateTime, product.ExpireDateTime));


    }
}
