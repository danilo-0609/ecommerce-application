using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Ratings.DeleteRating;

internal sealed class DeleteRatingCommandHandler 
    : ICommandRequestHandler<DeleteRatingCommand, ErrorOr<Unit>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public DeleteRatingCommandHandler(IRatingRepository ratingRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _ratingRepository = ratingRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteRatingCommand command, CancellationToken cancellationToken)
    {
        var ratingId = RatingId.Create(command.RatingId);

        var rating = await _ratingRepository.GetRatingById(ratingId);

        if (rating is null)
        {
            return Error.NotFound("Rating.NotFound", "The rating was not found");
        }

        if (rating.UserId.Value != _executionContextAccessor.UserId)
        {
            return Error.Unauthorized("User.Unauthorized", "Cannot delete if you are not authorized");
        }

        await _ratingRepository.DeleteAsync(rating);

        return Unit.Value;
    }
}
