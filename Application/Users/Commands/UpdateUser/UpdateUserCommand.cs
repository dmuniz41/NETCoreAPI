using System;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid UserId, string UserName, string Email) : IRequest;
