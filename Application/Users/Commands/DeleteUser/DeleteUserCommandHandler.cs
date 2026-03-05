using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{

    private readonly UserManager<User> _userManager;

    public DeleteUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.Single(x => x.Id == request.UserId);
        await _userManager.DeleteAsync(user);
    }
}
