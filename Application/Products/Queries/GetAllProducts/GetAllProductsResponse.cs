namespace Application.Products.Queries.GetAllProducts;

public sealed record GetAllProductsResponse(
    Guid ProductId, 
    string Name, 
    string Description, 
    decimal Price);
