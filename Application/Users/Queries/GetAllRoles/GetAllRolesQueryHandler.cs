using System;
using Application.Abstractions.Messaging;
using Application.Users.DTOs;
using Domain.Entities.Users;
using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleDto>>
{
    private readonly RoleManager<AppRole> _roleManager;
    public GetAllRolesQueryHandler(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<Result<List<RoleDto>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await Task.Run(() =>
            _roleManager.Roles
                .Select(r => new RoleDto(r.Id, r.Name!))
                .ToList(),
            cancellationToken);

        return Result.Success(roles);
    }
}
