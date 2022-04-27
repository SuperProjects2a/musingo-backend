using MediatR;
using musingo_backend.Queries.Message;
using musingo_backend.Repositories;
using musingo_backend.Models;

namespace musingo_backend.Handlers.MessageH;

public class GetMessagesByTransactionIdHandler : IRequestHandler<GetMessagesByTransactionIdQuery, HandlerResult<ICollection<Message>>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ITransactionRepository _transactionRepository;

    public GetMessagesByTransactionIdHandler(IMessageRepository messageRepository, ITransactionRepository transactionRepository)
    {
        _messageRepository = messageRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<HandlerResult<ICollection<Message>>> Handle(GetMessagesByTransactionIdQuery requst,
        CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetMessagesByTransactionId(requst.TransactionId);

        if (messages.Count == 0) return new HandlerResult<ICollection<Message>> { Status = 404 };

        var transaction = await _transactionRepository.GetTransaction(requst.TransactionId);

        if (transaction.Buyer.Id != requst.UserId && transaction.Seller.Id != requst.UserId)
            return new HandlerResult<ICollection<Message>> { Status = 403 };


        return new HandlerResult<ICollection<Message>> { Body = messages, Status = 200 };
    }
}