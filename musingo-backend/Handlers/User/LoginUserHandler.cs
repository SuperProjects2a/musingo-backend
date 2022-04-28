using MediatR;
using musingo_backend.Authentication;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public LoginUserHandler(IUserRepository userRepository, IJwtAuth jwtAuth)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.LoginUser(request.Email, request.Password);

        if (user is null) return new HandlerResult<User>() { Status = 404 };

        if (user.IsBanned) return new HandlerResult<User>() { Status = 1 };

        return new HandlerResult<User>() { Body = user, Status = 200 };
    }
}