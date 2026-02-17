using System;
using Domain.Entities.Products;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductRepository : IProductRepository
{
  private readonly ApplicationDbContext _context;

  public ProductRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public Task<Product?> GetByIdAsync(ProductId id)
  {
    return _context.Products
        .SingleOrDefaultAsync(p => p.Id == id);
  }

  public async Task<List<Product>> GetAllAsync(int offset, int limit)
  {
    return await _context.Products
        .Skip(offset)
        .Take(limit)
        .ToListAsync();
  }

  public void Add(Product product)
  {
    _context.Products.Add(product);
  }

  public void Update(Product product)
  {
    _context.Products.Update(product);
  }

  public void Remove(Product product)
  {
    _context.Products.Remove(product);
  }
}
