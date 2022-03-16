using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserById(int id);
    public Task<User?> LoginUser(string login, string password);
    public Task<User?> AddUser(User user);
    
}

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(RepositoryContext context) : base(context) { }


    public async Task<User?> GetUserById(int id)
    {
        var result = await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<User?> LoginUser(string login, string password)
    {
        var result = await GetAll().FirstOrDefaultAsync(x => x.Email == login);
        if (result is null)
            return result;
        var isValidPassword = BCrypt.Net.BCrypt.Verify(password, result.Password);
        if (isValidPassword)
        {
            return result;

        }
        return null;

    }

    public async Task<User?> AddUser(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var result = await AddAsync(user);
        return result;
    }
}