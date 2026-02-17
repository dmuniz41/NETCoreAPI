using System;
using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using Domain.Entities.Products;

namespace Application.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductResponse>
{
  private readonly IProductRepository _productRepository;
  public GetProductByIdQueryHandler(IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  public async Task<Result<GetProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId));

    if (product is null)
    {
      return Result.Failure<GetProductResponse>(
        new Error("Product.NotFound", $"The product with id {request.ProductId} was not found.")
      );
    }

    var response = new GetProductResponse(product.Id.Value, product.Name, string.Empty, product.Price.Amount);
    return response;
  }
}
