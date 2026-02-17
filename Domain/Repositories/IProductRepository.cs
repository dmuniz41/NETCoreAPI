using System;
using Domain.Entities.Products;

namespace Domain.Repositories;

public interface IProductRepository
{
  Task<Product?> GetByIdAsync(ProductId id);

  Task<List<Product>> GetAllAsync(int offset, int limit);

  void Add(Product product);

  void Update(Product product);

  void Remove(Product product);
}