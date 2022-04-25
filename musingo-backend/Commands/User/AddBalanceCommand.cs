using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class AddBalanceCommand : IRequest<HandlerResult<User>>
{
    public int UserId { get; set; }
    public double AmountToAdd { get; set; }
}