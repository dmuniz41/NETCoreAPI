using MediatR;

namespace Application.Users.Commands.UpdateUserRoles;

public sealed record UpdateUserRolesCommand
(
    Guid UserId,
    IReadOnlyList<Guid> RoleIds
) : IRequest;
