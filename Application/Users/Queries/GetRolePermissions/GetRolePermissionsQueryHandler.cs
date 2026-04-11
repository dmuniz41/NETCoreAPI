using Domain.Constants;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetRolePermissions;

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, IReadOnlyList<string>>
{
    private readonly RoleManager<AppRole> _roleManager;

    public GetRolePermissionsQueryHandler(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<IReadOnlyList<string>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
        {
            throw new InvalidOperationException("Role not found");
        }
        var claims = await _roleManager.GetClaimsAsync(role);
        return claims
            .Where(c => c.Type == ClaimTypes.Permission)
            .Select(c => c.Value)
            .ToList();
    }
}