using MediatR;

namespace Ecommerce.BuildingBlocks.Application.Queries;

public interface IQueryRequest<TResponse> : IRequest<TResponse>
{
}
