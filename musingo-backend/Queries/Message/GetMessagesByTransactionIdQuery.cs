using MediatR;

namespace musingo_backend.Queries.Message;

public class GetMessagesByTransactionIdQuery : IRequest<HandlerResult<ICollection<Models.Message>>>
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
}