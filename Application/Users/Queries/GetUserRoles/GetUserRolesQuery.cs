using Application.Abstractions.Messaging;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetUserRoles;

public sealed record GetUserRolesQuery(Guid UserId) : IQuery<List<RoleDto>>;