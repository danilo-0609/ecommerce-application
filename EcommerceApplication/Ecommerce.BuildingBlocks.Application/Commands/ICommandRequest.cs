using MediatR;

namespace Ecommerce.BuildingBlocks.Application.Commands;

public interface IQueryRequest<TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
