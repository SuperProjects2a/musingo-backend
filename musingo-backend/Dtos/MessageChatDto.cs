using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class MessageChatDto
{
    public UserDto Sender { get; set; }
    public TransactionDto Transaction { get; set; }
    public DateTime? SendTime { get; set; }
    public string? Text { get; set; }
    public int? UnReadMessagesCount { get; set; }
}