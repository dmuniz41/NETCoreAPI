using System;
using Application.Data;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(
      IProductRepository productRepository,
      IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            throw new InvalidOperationException("Product not found");
        }

        _productRepository.Remove(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
