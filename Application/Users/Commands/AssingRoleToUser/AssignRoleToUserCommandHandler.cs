using System;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.AssignRoleToUser;

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignUserRoleCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    public AssignRoleToUserCommandHandler(
        UserManager<User> userManager,
        RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task Handle(AssignUserRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var roleNames = new List<string>();

        foreach (var roleId in command.RoleIds)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} not found");
            }
            roleNames.Add(role.Name!);
        }

        var result = await _userManager.AddToRolesAsync(user, roleNames);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to assign roles to user");
        }
    }
}
