using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Ratings.GetAllRatingProductsByProductId;

internal sealed class GetAllRatingProductsByProductIdQueryHandler
    : IQueryRequestHandler<GetAllRatingProductsByProductIdQuery, ErrorOr<IReadOnlyList<RatingDto>>>
{
    private readonly IRatingRepository _ratingRepository;

    public GetAllRatingProductsByProductIdQueryHandler(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<RatingDto>>> Handle(GetAllRatingProductsByProductIdQuery query, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(query.ProductId);

        List<Rating>? ratings = await _ratingRepository.GetAllRatingsByProductIdAsync(productId);
        
        if (ratings is null)
        {
            return Error.NotFound("Rating.NotFound", "Ratings were not found in this product");
        }

        List<RatingDto> ratingDtos = new List<RatingDto>();

        ratings.ForEach(rating =>
        {
            var ratingDto = new RatingDto(
                rating.RatingId.Value,
                rating.RatingComment,
                rating.UserId.Value,
                rating.ProductId.Value,
                rating.RatingValue,
                rating.CreatedDateTime);

            ratingDtos.Add(ratingDto);
        });


        return ratingDtos.AsReadOnly();
    }
}
