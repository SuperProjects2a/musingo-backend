using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands.Admin;

public class BanUnbanUserCommand: IRequest<HandlerResult<User>>
{
    public string Email { get; set; }
    public int AdminId { get; set; }
}