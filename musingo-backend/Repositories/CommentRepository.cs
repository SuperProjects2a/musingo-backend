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
    public Task<UserComment> RemoveCommentById(int id);
    public Task<UserComment> IsCommented(int transactionId,int userId);
}
public class CommentRepository:Repository<UserComment>, ICommentRepository
{
    public CommentRepository(RepositoryContext context) : base(context) { }
    public async Task<UserComment> GetCommentById(int id)
    {
       var result = await GetAll()
           .Include(x=>x.Transaction)
           .ThenInclude(x=>x.Seller)
           .Include(x=>x.Transaction)
           .ThenInclude(x=>x.Buyer)
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
        var result = await repositoryContext.UserComments.FirstOrDefaultAsync(x => x.Transaction.Id == transactionId);
        return result;
    }


    public async Task<UserComment> UpdateComment(UserComment userComment)
    {
        
        var result = await UpdateAsync(userComment);
        return result;
    }

    public async Task<UserComment> RemoveCommentById(int transactionId)
    {
        var userComment = GetAll().FirstOrDefault(x => x.Id == transactionId);
        if (userComment is null)
            return userComment;
        var result = await RemoveAsync(userComment);
        return result;
    }

}