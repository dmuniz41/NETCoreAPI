using Domain.Entities.Products;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public record class UpdateProductCommand(Guid ProductId, string Name, Money Price, string Sku) : IRequest;
