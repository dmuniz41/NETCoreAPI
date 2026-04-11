using MediatR;

namespace Application.Users.Commands.AssignRoleToUser;

public record AssignUserRoleCommand(
    Guid UserId,
    IReadOnlyList<Guid> RoleIds
) : IRequest;
