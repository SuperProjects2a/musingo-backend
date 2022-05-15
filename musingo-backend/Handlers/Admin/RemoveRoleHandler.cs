using MediatR;
using musingo_backend.Commands.Admin;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Admin;

public class RemoveRoleHandler: IRequestHandler<RemoveRoleCommand,HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public RemoveRoleHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null) return new HandlerResult<User> { Status = 404 };

        user.Role &= ~request.Role;

        await _userRepository.UpdateUser(user);

        return new HandlerResult<User> { Body = user, Status = 200 };
    }
}