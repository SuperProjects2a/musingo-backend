using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class LoginUserCommand: IRequest<HandlerResult<User>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}