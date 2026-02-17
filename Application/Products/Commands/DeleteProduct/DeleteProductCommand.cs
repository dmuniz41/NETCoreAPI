using System;
using Domain.Entities.Products;
using MediatR;

namespace Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(ProductId ProductId) : IRequest;