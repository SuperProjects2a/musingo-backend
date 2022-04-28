using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands.Admin;

public class BanUnbanUserCommand: IRequest<HandlerResult<User>>
{
    public int UserId { get; set; }
}