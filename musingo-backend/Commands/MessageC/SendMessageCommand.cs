using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands.MessageC;

public class SendMessageCommand : IRequest<HandlerResult<Message>>
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
    public string? Text { get; set; }
}