using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<User>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        result.Body = user;
        return result;
    }
}