using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Users.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly RoleManager<AppRole> _roleManager;
    public CreateRoleCommandHandler(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var exists = await _roleManager.RoleExistsAsync(request.Name);
        if (exists)
        {
            throw new InvalidOperationException("Role already exists");
        }
        var role = new AppRole { Name = request.Name };
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to create role");
        }
        if (!string.IsNullOrEmpty(request.Description))
        {
            await _roleManager.AddClaimAsync(role, new Claim("description", request.Description));
        }
        if (request.Permissions?.Count > 0)
        {
            var validPermissions = Domain.ValueObjects.Permissions.All;
            var invalidPermissions = request.Permissions.Where(p => !validPermissions.Contains(p)).ToList();

            if (invalidPermissions.Any())
            {
                throw new InvalidOperationException($"Invalid permissions: {string.Join(", ", invalidPermissions)}");
            }
            foreach (var permission in request.Permissions)
            {
                await _roleManager.AddClaimAsync(role, new Claim(Domain.Constants.ClaimTypes.Permission, permission));
            }
        }
    }
}