using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class PurchaseFromTransactionCommand : IRequest<HandlerResult<Transaction>>
{
    public int UserId { get; set; }
    public int TransactionId { get; set; }
}