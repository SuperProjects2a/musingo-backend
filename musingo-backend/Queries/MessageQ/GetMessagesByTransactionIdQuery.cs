using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries.MessageQ;

public class GetMessagesByTransactionIdQuery : IRequest<HandlerResult<ICollection<MessageDto>>>
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
}