using MediatR;
using Microsoft.Identity.Client;
using musingo_backend.Commands.MessageC;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers.MessageH;

public class SendMessageHandler: IRequestHandler<SendMessageCommand,HandlerResult<Message>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;

    public SendMessageHandler(IMessageRepository messageRepository, ITransactionRepository transactionRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<Message>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var transaction = await  _transactionRepository.GetTransaction(request.TransactionId);
        
        if (transaction is null) return new HandlerResult<Message> { Status = 404 };

        if (transaction.Buyer.Id != request.UserId && transaction.Seller.Id != request.UserId)
            return new HandlerResult<Message> { Status = 403 };

        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return new HandlerResult<Message> { Status = 404 };

        var message = new Message
        {
            Transaction = transaction,
            Sender = user,
            Text = request.Text
        };
        await _messageRepository.SendMessage(message);

        return new HandlerResult<Message> { Body = message, Status = 200 };


    }
}