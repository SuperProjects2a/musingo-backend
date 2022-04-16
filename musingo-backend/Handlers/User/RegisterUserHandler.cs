using AutoMapper;
using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<User>();
        var user = new User()
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password
        };
        await _userRepository.AddUser(user);
        result.Body = user;
        return result;
    }
}