using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Repositories;

public interface ICommentRepository
{
    public Task<UserComment> GetCommentById(int id);
    public Task<UserComment> AddComment(UserComment userComment);
    public Task<UserComment> UpdateComment(UserComment userComment);
    public Task<UserComment> RemoveComment(UserComment userComment);
    public Task<UserComment> IsCommented(int transactionId,int userId);

    public Task<ICollection<UserComment>> GetUserComments(int userId);
    public Task<ICollection<UserComment>> GetUserRatings(int userId);
}
public class CommentRepository:Repository<UserComment>, ICommentRepository
{
    public CommentRepository(RepositoryContext context) : base(context) { }
    public async Task<UserComment> GetCommentById(int id)
    {
       var result = await GetAll()
           .Include(x=>x.User)
           .Include(x=>x.Transaction)
           .FirstOrDefaultAsync(x=>x.Id == id);
       return result;
    }

    public async Task<UserComment> AddComment(UserComment userComment)
    {
        var result = await AddAsync(userComment);
        return result;
    }

    public async Task<UserComment> IsCommented(int transactionId,int userId)
    {
        var result = await repositoryContext.UserComments.FirstOrDefaultAsync(x => x.Transaction.Id == transactionId && x.User.Id == userId);
        return result;
    }

    public async Task<ICollection<UserComment>> GetUserComments(int userId)
    {
        var result = await GetAll()
            .Include(x=>x.Transaction)
            .Include(x => x.User)
            .Where(x => x.User.Id == userId).ToListAsync();
        return result;
    }

    public async Task<ICollection<UserComment>> GetUserRatings(int userId)
    {
        var result = await repositoryContext.UserComments
            .Include(x=>x.User)
            .Include(x=>x.Transaction)
            .Where(x => x.Transaction.Buyer.Id == userId || x.Transaction.Seller.Id == userId)
            .Where(x => x.User.Id != userId)
            .ToListAsync();
        return result; 
    }


    public async Task<UserComment> UpdateComment(UserComment userComment)
    {
        var result = await UpdateAsync(userComment);
        return result;
    }

    public async Task<UserComment> RemoveComment(UserComment userComment)
    {
        var result = await RemoveAsync(userComment);
        return result;
    }

}