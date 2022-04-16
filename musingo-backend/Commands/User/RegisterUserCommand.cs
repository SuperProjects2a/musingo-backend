using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class RegisterUserCommand: IRequest<HandlerResult<User>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool AcceptedTOS { get; set; }
    public string Password { get; set; }
}