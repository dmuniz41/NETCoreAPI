using System;

namespace Application.Users.Queries.GetUserByIdQuery;

public sealed record GetUserByIdQueryResponse(Guid UserId, string UserName, string Email);


