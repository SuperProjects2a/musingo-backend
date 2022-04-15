using MediatR;
using musingo_backend.Authentication;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class LoginUserHandler: IRequestHandler<LoginUserCommand,User?>
{
    private readonly IUserRepository _userRepository;

    public LoginUserHandler(IUserRepository userRepository, IJwtAuth jwtAuth)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        
        return await _userRepository.LoginUser(request.Email, request.Password);
    }
}