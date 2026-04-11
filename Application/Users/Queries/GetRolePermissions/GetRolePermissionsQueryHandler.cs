using Application.Abstractions.Messaging;
using Domain.Constants;
using Domain.Entities.Users;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetRolePermissions;

public class GetRolePermissionsQueryHandler : IQueryHandler<GetRolePermissionsQuery, IReadOnlyList<string>>
{
    private readonly RoleManager<AppRole> _roleManager;

    public GetRolePermissionsQueryHandler(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<IReadOnlyList<string>>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
        {
            return Result.Failure<IReadOnlyList<string>>(new Error("Role.NotFound", "Role not found"));
        }

        var claims = await _roleManager.GetClaimsAsync(role);
        var permissions = claims
            .Where(c => c.Type == ClaimTypes.Permission)
            .Select(c => c.Value)
            .ToList();

        return Result.Success<IReadOnlyList<string>>(permissions);
    }
}