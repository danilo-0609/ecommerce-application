namespace Ecommerce.Catalog.Domain.Sales;

public interface ISaleRepository
{
    Task AddAsync(Sale sale);

    Task<Sale?> GetByIdAsync(SaleId saleId);
}
