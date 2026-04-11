using Application.Abstractions.Messaging;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetAllRoles;

public sealed record GetAllRolesQuery : IQuery<List<RoleDto>>;
