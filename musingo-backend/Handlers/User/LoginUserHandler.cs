using MediatR;
using musingo_backend.Authentication;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class LoginUserHandler: IRequestHandler<LoginUserCommand, HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public LoginUserHandler(IUserRepository userRepository, IJwtAuth jwtAuth)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<User>();
        var user = await _userRepository.LoginUser(request.Email, request.Password);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        result.Body = user;
        return result;
    }
}