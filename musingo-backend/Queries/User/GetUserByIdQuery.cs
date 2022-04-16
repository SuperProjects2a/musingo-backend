using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetUserByIdQuery: IRequest<HandlerResult<User>>
{
    public int UserId { get; set; }
}