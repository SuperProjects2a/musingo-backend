using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserById(int id);
    public Task<User?> LoginUser(string login, string password);
    public Task<User?> AddUser(User user);

    public Task<User?> UpdateUser(User user);
    public Task<double> GetAvg(int id);


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
        var users = GetAll();
        var withEmail = users.Where(x => x.Email == user.Email).ToList();
        if (withEmail.Count > 0)
        {
            return null;
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var result = await AddAsync(user);
        return result;
    }

    public async Task<User?> UpdateUser(User user)
    {
        var result = await UpdateAsync(user);
        return result;
    }

    public async Task<double> GetAvg(int id)
    {
        var transactions = await repositoryContext.UserComments
            .Where(x => x.Transaction.Buyer.Id == id || x.Transaction.Seller.Id == id)
            .Where(x => x.User.Id != id)
            .ToListAsync();
        if (transactions.Count == 0)
            return 0;
        var rating = transactions.Average(x => x.Rating);
        return rating;
    }

}