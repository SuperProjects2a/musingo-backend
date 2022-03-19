using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;

public interface ICommentRepository
{
    public Task<UserComment> GetCommentById(int id);
    public Task<UserComment> AddComment(UserComment userComment);
    public Task<UserComment> UpdateComment(UserComment userComment);
    public Task<UserComment> RemoveComment(UserComment userComment);
}
public class CommentRepository:Repository<UserComment>, ICommentRepository
{
    public CommentRepository(RepositoryContext context) : base(context) { }
    public async Task<UserComment> GetCommentById(int id)
    {
        var result = await GetAll().FirstOrDefaultAsync(x=>x.Id == id);
        return result;
    }

    public async Task<UserComment> AddComment(UserComment userComment)
    {
        var result = await AddAsync(userComment);
        return result;
    }

    public async Task<UserComment> UpdateComment(UserComment userComment)
    {
        var result = await UpdateAsync(userComment);
        return result;
    }

    public async Task<UserComment> RemoveComment(UserComment userComment)
    {
        var result = await DeleteAsync(userComment);
        return result;
    }
}