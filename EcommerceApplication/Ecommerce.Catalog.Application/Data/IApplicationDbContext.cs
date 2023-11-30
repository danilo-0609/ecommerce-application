using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using Ecommerce.Catalog.Domain.Sales;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Application.Data;

public interface IApplicationDbContext : IUnitOfWork
{
    DbSet<Product> Products { get; set; }

    DbSet<Comment> Comments { get; set; }

    DbSet<Sale> Sale { get; set; }

    DbSet<Rating> Ratings { get; set; }
}
