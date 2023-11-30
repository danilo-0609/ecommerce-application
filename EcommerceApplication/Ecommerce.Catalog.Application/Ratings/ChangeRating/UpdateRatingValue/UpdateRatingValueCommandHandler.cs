using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using ErrorOr;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ecommerce.Catalog.Application.Ratings.ChangeRating.UpdateRatingValue;

internal sealed class UpdateRatingValueCommandHandler
    : ICommandRequestHandler<UpdateRatingValueCommand, ErrorOr<Guid>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IExecutionContextAccessor _contextAccessor;

    public UpdateRatingValueCommandHandler(IRatingRepository commentRepository,
        IExecutionContextAccessor contextAccessor)
    {
        _ratingRepository = commentRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Guid>> Handle(UpdateRatingValueCommand command, CancellationToken cancellationToken)
    {
        var ratingId = RatingId.Create(command.RatingId);

        var rating = await _ratingRepository.GetRatingById(ratingId);

        if (rating is null)
        {
            return Error.NotFound("Rating.NotFound", "The rating with the id provided was not found");
        }

        var productId = ProductId.Create(command.ProductId);

        var ratingUpdated = Rating.Update(
            ratingId.Value, 
            command.RatingValue,
            new UserId(_contextAccessor.UserId),
            productId,
            rating.CreatedDateTime,
            DateTime.UtcNow,
            rating.RatingComment);


        await _ratingRepository.UpdateAsync(ratingUpdated, cancellationToken);

        return ratingId.Value;
    }
}
