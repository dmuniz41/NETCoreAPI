using System;
using Application.Data;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.RegisterUser;

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email
        };

        await _userManager.CreateAsync(user, request.Password);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
