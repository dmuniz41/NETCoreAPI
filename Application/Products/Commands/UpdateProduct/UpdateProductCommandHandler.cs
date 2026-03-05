using System;
using Application.Data;
using Domain.Entities.Products;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var productId = new ProductId(command.ProductId);

        var productToUpdate = await _productRepository.GetByIdAsync(productId);

        if (productToUpdate is null)
        {
            throw new InvalidOperationException("Product not found");
        }

        var sku = Sku.Create(command.Sku);
        productToUpdate.Update(command.Name, command.Price, sku);

        _productRepository.Update(productToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
