using Application.Abstractions.Messaging;

namespace Application.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery(int Offset = 0, int Limit = 10) : IQuery<List<GetAllProductsResponse>>;
