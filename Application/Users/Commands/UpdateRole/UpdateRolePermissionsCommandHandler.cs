using System.Security.Claims;
using Domain.Entities.Users;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.UpdateRole;

public class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand>
{
    private readonly RoleManager<AppRole> _roleManager;

    public UpdateRolePermissionsCommandHandler(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
        {
            throw new InvalidOperationException("Role not found");
        }
        var validPermissions = Permissions.All;
        var invalidPermissions = request.Permissions.Where(p => !validPermissions.Contains(p)).ToList();

        if (invalidPermissions.Any())
        {
            throw new InvalidOperationException($"Invalid permissions: {string.Join(", ", invalidPermissions)}");
        }
        var existingClaims = await _roleManager.GetClaimsAsync(role);
        var permissionClaims = existingClaims.Where(c => c.Type == Domain.Constants.ClaimTypes.Permission).ToList();

        foreach (var claim in permissionClaims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }
        foreach (var permission in request.Permissions)
        {
            await _roleManager.AddClaimAsync(role, new Claim(Domain.Constants.ClaimTypes.Permission, permission));
        }
    }
}
