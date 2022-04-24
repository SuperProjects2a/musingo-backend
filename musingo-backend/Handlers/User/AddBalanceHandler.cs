using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddBalanceHandler : IRequestHandler<AddBalanceCommand, HandlerResult<User>>
{
    private IUserRepository _userRepository;

    public AddBalanceHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(AddBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return new HandlerResult<User>() {Status = 404};
        user.WalletBalance += request.AmountToAdd;

        var result = await _userRepository.UpdateUser(user);
        return new HandlerResult<User>() {Body = result, Status = 200};
    }
}