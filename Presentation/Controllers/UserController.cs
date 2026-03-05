using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetUserByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        public UserController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(userId);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(userId);
            await Sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}
