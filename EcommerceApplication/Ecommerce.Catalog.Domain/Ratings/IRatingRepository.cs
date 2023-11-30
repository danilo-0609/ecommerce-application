using Ecommerce.Catalog.Domain.Products;

namespace Ecommerce.Catalog.Domain.Ratings;

public interface IRatingRepository
{
    Task AddAsync(Rating rating);

    Task<Rating?> GetRatingById(RatingId ratingId); 

    Task UpdateAsync(Rating rating, CancellationToken cancellationToken = default);

    Task DeleteAsync(Rating rating);

    Task<List<Rating>?> GetAllRatingsByProductIdAsync(ProductId productId);
}
