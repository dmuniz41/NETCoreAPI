
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IRequest;
