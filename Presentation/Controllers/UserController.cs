using Application.Users.Commands.CreateRole;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Commands.AssignRoleToUser;
using Application.Users.Commands.UpdateUserRoles;
using Application.Users.Queries.GetPermissions;
using Application.Users.Queries.GetRolePermissions;
using Application.Users.Queries.GetUserByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Application.Users.Queries.GetUserRoles;
using Application.Users.Queries.GetAllRoles;
using Application.Users.Commands.UpdateRole;

namespace Presentation.Controllers
{
    [Route("api/users")]
    public class UserController : ApiController
    {
        public UserController(ISender sender) : base(sender)
        {
        }

        // Users

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
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

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(userId);
            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssingRoleToUser(AssignUserRoleCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPut("roles")]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet("{userId:guid}/roles")]
        public async Task<IActionResult> GetUserRoles(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserRolesQuery(userId);
            var result = await Sender.Send(query, cancellationToken);

            return Ok(result);
        }

        // Roles

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var query = new GetAllRolesQuery();
            var result = await Sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
        {
            var query = new GetPermissionsQuery();
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("roles/{roleId:guid}/permissions")]
        public async Task<IActionResult> GetRolePermissions(Guid roleId, CancellationToken cancellationToken)
        {
            var query = new GetRolePermissionsQuery(roleId);
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("roles/permissions")]
        public async Task<IActionResult> UpdateRolePermissions(UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
        {
            await Sender.Send(command, cancellationToken);
            return Ok();
        }
    }
}
