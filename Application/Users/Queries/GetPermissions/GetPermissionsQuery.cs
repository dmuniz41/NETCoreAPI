using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Queries.GetPermissions;

public record class GetPermissionsQuery : IRequest<IReadOnlyList<string>>
{
    public IReadOnlyList<string> Handle() => Permissions.All;
}
