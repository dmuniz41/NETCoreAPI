using System;
using MediatR;

namespace Application.Users.Commands.DeleteRole;

public record DeleteRoleCommand(Guid RoleId) : IRequest;
