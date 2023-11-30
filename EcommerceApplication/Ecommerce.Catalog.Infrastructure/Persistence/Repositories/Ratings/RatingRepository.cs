using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories.Ratings;

internal sealed class RatingRepository : IRatingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RatingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Rating rating)
    {
        await _dbContext.Ratings.AddAsync(rating);
    }

    public async Task DeleteAsync(Rating rating)
    {
        await _dbContext.Ratings
            .Where(x => x.RatingId == rating.RatingId)
            .ExecuteDeleteAsync();

    }

    public async Task<List<Rating>?> GetAllRatingsByProductIdAsync(ProductId productId)
    {
        List<Rating> ratings = await _dbContext
            .Ratings
            .Where(x => x.ProductId == productId)
            .ToListAsync();

        if (ratings.Count == 0)
        {
            return null;
        }

        return ratings;
    }

    public async Task<Rating?> GetRatingById(RatingId ratingId)
    {
        Rating? rating = await _dbContext
            .Ratings
            .Where(x => x.RatingId == ratingId)
            .SingleOrDefaultAsync();

        if (rating is null)
        {
            return null;
        }

        return rating;
    }

    public async Task UpdateAsync(Rating rating, CancellationToken cancellationToken = default)
    {
        await _dbContext
            .Ratings
            .Where(d => d.RatingId == rating.RatingId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(g => g.RatingId, rating.RatingId)
                       .SetProperty(g => g.RatingComment, rating.RatingComment)
                       .SetProperty(g => g.UserId, rating.UserId)
                       .SetProperty(g => g.ProductId, rating.ProductId)
                       .SetProperty(g => g.RatingValue, rating.RatingValue)
                       .SetProperty(g => g.CreatedDateTime, rating.CreatedDateTime)
                       .SetProperty(g => g.UpdatedDateTime, rating.UpdatedDateTime));
    }
}
