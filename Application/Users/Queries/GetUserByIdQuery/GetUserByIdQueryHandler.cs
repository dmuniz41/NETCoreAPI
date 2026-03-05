using System;
using Application.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetUserByIdQuery;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{

    private readonly UserManager<User> _userManager;

    public GetUserByIdQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return Result.Failure<GetUserByIdQueryResponse>(
                new Error("User.NotFound", $"The user with id {request.UserId} was not found.")
            );
        }

        var response = new GetUserByIdQueryResponse(user.Id, user.UserName, user.Email);
        return response;
    }
}
