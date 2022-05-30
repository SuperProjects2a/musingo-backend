using System.Collections;
using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Queries.MessageQ;
using musingo_backend.Repositories;
using musingo_backend.Models;

namespace musingo_backend.Handlers.MessageH;

public class GetMessagesByTransactionIdHandler : IRequestHandler<GetMessagesByTransactionIdQuery, HandlerResult<ICollection<MessageDto>>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IMapper _mapper;

    public GetMessagesByTransactionIdHandler(IMessageRepository messageRepository, ITransactionRepository transactionRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _transactionRepository = transactionRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<ICollection<MessageDto>>> Handle(GetMessagesByTransactionIdQuery requst,
        CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetMessagesByTransaction(requst.TransactionId);

        if (messages.Count == 0) return new HandlerResult<ICollection<MessageDto>> { Status = 404 };

        var transaction = await _transactionRepository.GetTransaction(requst.TransactionId);

        if (transaction.Buyer.Id != requst.UserId && transaction.Seller.Id != requst.UserId)
            return new HandlerResult<ICollection<MessageDto>> { Status = 403 };

        var unreadMessages = messages
            .Where(x => x.Sender.Id != requst.UserId && x.IsRead == false)
            .ToList();
        unreadMessages.ForEach(r => r.IsRead = true);
        await _messageRepository.UpdateMessageRange(unreadMessages);

        var messageDto = _mapper.Map<ICollection<MessageDto>>(messages).ToList();
        if (messageDto.Count > 0)
            messageDto[0].Transaction.Offer.ImageUrl =
                await _imageUrlRepository.GetFirstImageUrlByOfferId(messageDto[0].Transaction.Offer.Id);

        return new HandlerResult<ICollection<MessageDto>> { Body = messageDto, Status = 200 };
    }
}