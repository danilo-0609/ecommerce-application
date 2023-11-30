using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Ratings.RatingProduct;

internal sealed class AddRatingProductCommandHandler
    : ICommandRequestHandler<AddRatingProductCommand, ErrorOr<Guid>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IExecutionContextAccessor _contextAccessor;

    public AddRatingProductCommandHandler(IRatingRepository ratingRepository, IExecutionContextAccessor contextAccessor)
    {
        _ratingRepository = ratingRepository ?? throw new ArgumentNullException(nameof(ratingRepository));
        _contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Guid>> Handle(AddRatingProductCommand command, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(command.ProductId);

        var rating = Rating.Create(
            command.RatingValue,
            new UserId(_contextAccessor.UserId),
            productId,
            DateTime.UtcNow,
            command.RatingComment);

        await _ratingRepository.AddAsync(rating);

        return rating.RatingId.Value;
    }
}
