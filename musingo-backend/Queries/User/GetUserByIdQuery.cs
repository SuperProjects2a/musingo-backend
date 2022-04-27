using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetUserByIdQuery: IRequest<HandlerResult<UserDetailsDto>>
{
    public int UserId { get; set; }
}