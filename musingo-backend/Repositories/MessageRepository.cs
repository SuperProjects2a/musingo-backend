using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;


public interface IMessageRepository
{
    public Task<ICollection<Message>> GetMessagesByTransactionId(int transactionId);

    public Task<Message> SendMessage(Message message);


}



public class MessageRepository:Repository<Message>,IMessageRepository
{
    public MessageRepository(RepositoryContext context) : base(context)
    {

    }

    public async Task<ICollection<Message>> GetMessagesByTransactionId(int transactionId)
    {
        var messages = await GetAll()
            .Include(x => x.Transaction)
            .Include(x => x.Sender)
            .Where(x => x.Transaction.Id == transactionId)
            .ToListAsync();

        return messages;
    }

    public async Task<Message> SendMessage(Message message)
    {
        return await AddAsync(message);
    }
}