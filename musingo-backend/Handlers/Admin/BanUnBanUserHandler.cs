using MediatR;
using musingo_backend.Commands.Admin;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Admin;

public class BanUnBanUserHandler : IRequestHandler<BanUnBanUserCommand,HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public BanUnBanUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(BanUnBanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null) return new HandlerResult<User> { Status = 404 };

        user.IsBanned = !user.IsBanned;

        await _userRepository.UpdateUser(user);

        return new HandlerResult<User> { Body = user, Status = 200 };
    }
}