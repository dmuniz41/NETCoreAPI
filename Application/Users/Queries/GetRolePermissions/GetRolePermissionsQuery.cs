using System;
using MediatR;

namespace Application.Users.Queries.GetRolePermissions;

public sealed record GetRolePermissionsQuery(Guid RoleId) : IRequest<IReadOnlyList<string>>;
