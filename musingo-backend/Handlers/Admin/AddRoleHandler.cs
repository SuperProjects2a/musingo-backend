using MediatR;
using musingo_backend.Commands.Admin;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Admin;

public class AddRoleHandler : IRequestHandler<AddRoleCommand,HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public AddRoleHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<HandlerResult<User>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null) return new HandlerResult<User> { Status = 404 };

        user.Role |= request.Role;

        await _userRepository.UpdateUser(user);

        return new HandlerResult<User>{Body = user,Status = 200};
    }
}