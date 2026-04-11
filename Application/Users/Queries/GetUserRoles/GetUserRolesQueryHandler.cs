using System;
using Application.Abstractions.Messaging;
using Application.Users.DTOs;
using Domain.Entities.Users;
using Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUserRoles;

public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, List<RoleDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    public GetUserRolesQueryHandler(
        UserManager<User> userManager,
        RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<Result<List<RoleDto>>> Handle(
        GetUserRolesQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Result.Failure<List<RoleDto>>(new Error("User.NotFound", "User not found"));
        }

        var roleNames = await _userManager.GetRolesAsync(user);

        var roles = new List<RoleDto>();

        foreach (var roleName in roleNames)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role is not null)
            {
                roles.Add(new RoleDto(role.Id, role.Name!));
            }
        }
        return Result.Success(roles);
    }
}
