using System;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Queries.GetPermissions;

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IReadOnlyList<string>>
{
    public Task<IReadOnlyList<string>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Permissions.All);
    }
}
