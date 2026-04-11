using System;
using Application.Abstractions.Messaging;
using MediatR;

namespace Application.Users.Queries.GetRolePermissions;

public sealed record GetRolePermissionsQuery(Guid RoleId) : IQuery<IReadOnlyList<string>>;
