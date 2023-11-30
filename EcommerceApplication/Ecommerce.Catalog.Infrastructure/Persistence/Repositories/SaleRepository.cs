using Ecommerce.Catalog.Domain.Sales;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories;

internal sealed class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SaleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Sale sale)
    {
        await _dbContext.Sale.AddAsync(sale);
    }

    public async Task<Sale?> GetByIdAsync(SaleId saleId)
    {
        return await _dbContext
            .Sale
            .Where(u => u.SaleId == saleId)
            .SingleOrDefaultAsync();
    }
}
