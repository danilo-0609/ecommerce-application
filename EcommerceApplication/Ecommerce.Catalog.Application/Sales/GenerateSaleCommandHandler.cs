using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Sales;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Sales;

internal sealed class GenerateSaleCommandHandler : ICommandRequestHandler<GenerateSaleCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;

    public GenerateSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Unit> Handle(GenerateSaleCommand command, CancellationToken cancellationToken)
    {
        Sale sale = Sale.Generate(
            command.ProductId,
            command.Price,
            command.UserId,
            DateTime.UtcNow);

        await _saleRepository.AddAsync(sale);

        return Unit.Value;
    }
}
