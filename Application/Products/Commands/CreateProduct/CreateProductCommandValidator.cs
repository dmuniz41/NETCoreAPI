using System;
using FluentValidation;

namespace Application.Products.Commands.CreateProduct;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Sku).NotEmpty();
    RuleFor(x => x.Amount).GreaterThan(0);
  }
}
