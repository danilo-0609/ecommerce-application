using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.BuildingBlocks.Domain;
using MediatR;
using System.Transactions;

namespace Ecommerce.BuildingBlocks.Application.Abstractions;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQueryRequest<TResponse>
    where TResponse : notnull
{
    private readonly IUnitOfWorkBase _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWorkBase unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        using (var transactionScope = new TransactionScope())
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transactionScope.Complete();

            return response;
        }
    }
}
