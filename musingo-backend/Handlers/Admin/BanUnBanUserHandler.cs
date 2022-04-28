using MediatR;
using musingo_backend.Commands.Admin;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Admin;

public class BanUnbanUserHandler : IRequestHandler<BanUnbanUserCommand,HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public BanUnbanUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(BanUnbanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null) return new HandlerResult<User> { Status = 404 };

        user.IsBanned = !user.IsBanned;

        await _userRepository.UpdateUser(user);

        return new HandlerResult<User> { Body = user, Status = 200 };
    }
}