using System;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    public UpdateUserRolesCommandHandler(
        UserManager<User> userManager,
        RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task Handle(UpdateUserRolesCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Count > 0)
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
        }

        if (command.RoleIds.Count == 0)
        {
            return;
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
