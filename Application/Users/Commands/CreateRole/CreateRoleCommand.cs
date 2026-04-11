using MediatR;

namespace Application.Users.Commands.CreateRole;

public sealed record CreateRoleCommand(
    string Name,
    string? Description = null,
    IReadOnlyList<string>? Permissions = null) : IRequest;
