using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Queries.GetAllProducts;
using Presentation.Abstractions;
using Application.Products.Queries.GetProductById;
using Application.Products.Commands.UpdateProduct;

namespace Presentation.Controllers
{
    [Route("api/products")]
    public class ProductController : ApiController
    {

        public ProductController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct(CreateProductCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetAllProductsQuery(offset, limit);
            var result = await Sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid productId, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(productId);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);
            return Ok();
        }
    }
}
