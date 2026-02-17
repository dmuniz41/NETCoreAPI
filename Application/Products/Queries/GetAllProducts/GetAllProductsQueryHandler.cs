using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Entities.Products;
using Domain.Shared;

namespace Application.Products.Queries.GetAllProducts;

internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, List<GetAllProductsResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<List<GetAllProductsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(request.Offset, request.Limit);

        var response = products.Select(p => new GetAllProductsResponse(
            p.Id.Value,
            p.Name,
            string.Empty,
            p.Price.Amount
        )).ToList();

        return Result.Success(response);
    }
}
