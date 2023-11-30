using MediatR;

namespace Ecommerce.BuildingBlocks.Application.Commands;

public interface ICommandRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQueryRequest<TResponse>
    where TResponse : notnull
{
}
