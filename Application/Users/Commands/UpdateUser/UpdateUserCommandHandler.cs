using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.UpdateUser;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;

    public UpdateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.Single(x => x.Id == request.UserId);

        user.UserName = request.UserName;
        user.Email = request.Email;

        await _userManager.UpdateAsync(user);
    }
}
