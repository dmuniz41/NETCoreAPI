namespace Application.Products.Queries.GetProductById;

public sealed record GetProductResponse(Guid ProductId, string Name, string Description, decimal Price);