using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;


public interface IMessageRepository
{
    public Task<ICollection<Message>> GetMessagesByTransaction(int transactionId);
    public Task<Message> SendMessage(Message message);
    public Task<ICollection<Message>> UpdateMessageRange(ICollection<Message> messages);
    public Task<int> UnreadMessageCount(int transactionId, int userId);

    public Task<ICollection<Message>> GetLatestMessages(int userId);


}



public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(RepositoryContext context) : base(context)
    {

    }

    public async Task<ICollection<Message>> GetMessagesByTransaction(int transactionId)
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

    public async Task<ICollection<Message>> UpdateMessageRange(ICollection<Message> messages)
    {
        return await UpdateRangeAsync(messages);
    }

    public async Task<int> UnreadMessageCount(int transactionId, int userId)
    {
        return await GetAll()
            .Include(x => x.Transaction)
            .Include(x => x.Sender)
            .Where(x => x.Transaction.Id == transactionId && x.Sender.Id != userId && x.IsRead == false)
            .CountAsync();
    }

    public async Task<ICollection<Message>> GetLatestMessages(int userId)
    {
        var messages = await GetAll()
            .Include(x => x.Transaction)
            .ThenInclude(x=>x.Buyer)
            .Include(x=>x.Transaction)
            .ThenInclude(x=>x.Seller)
            .Include(x=>x.Transaction)
            .ThenInclude(x=>x.Offer)
            .Include(x => x.Sender)
            .Where(x => (x.Transaction.Buyer.Id == userId || x.Transaction.Seller.Id == userId))
            .ToListAsync();

        var latestMessages = messages.GroupBy(x => x.Transaction.Id, (key, g) => g.OrderByDescending(e => e.SendTime).FirstOrDefault()).ToList();
        return latestMessages; 

    }
}