using System;
using MediatR;

namespace Application.Users.Commands.UpdateRole;

public record class UpdateRolePermissionsCommand(
    Guid RoleId,
    IReadOnlyList<string> Permissions) : IRequest;
