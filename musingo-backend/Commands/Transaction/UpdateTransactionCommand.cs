using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class UpdateTransactionCommand : IRequest<HandlerResult<Transaction>>
{
    public int UserId { get; set; }
    public int TransactionId { get; set; }
    public double Cost { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
}