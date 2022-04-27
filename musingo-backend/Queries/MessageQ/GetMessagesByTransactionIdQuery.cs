using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries.MessageQ;

public class GetMessagesByTransactionIdQuery : IRequest<HandlerResult<ICollection<Message>>>
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
}