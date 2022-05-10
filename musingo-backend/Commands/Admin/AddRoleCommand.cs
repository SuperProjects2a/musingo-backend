using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands.Admin;

public class AddRoleCommand: IRequest<HandlerResult<User>>
{
    public int UserId { get; set; }
    public Role Role { get; set; }
}