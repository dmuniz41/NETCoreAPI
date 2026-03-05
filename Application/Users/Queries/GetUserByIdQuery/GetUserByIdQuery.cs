using System;
using Application.Abstractions.Messaging;

namespace Application.Users.Queries.GetUserByIdQuery;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<GetUserByIdQueryResponse>
{

}
