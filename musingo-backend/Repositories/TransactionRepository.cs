

using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories
{
    public interface ITransactionRepository
    {
        public Task<Transaction?> GetTransaction(int id);
        public Task<Transaction> AddTransaction(Transaction transaction);
        public Task<Transaction> UpdateTransaction(Transaction transaction);
        public IQueryable<Transaction> GetAllTransactions();
    }
    public class TransactionRepository:Repository<Transaction>,ITransactionRepository
    {
        public TransactionRepository(RepositoryContext context) : base(context) { }
        public async  Task<Transaction?> GetTransaction(int id)
        {
            var result = await repositoryContext.Transactions
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            var result = await AddAsync(transaction);
            return result;
        }

        public async Task<Transaction> UpdateTransaction(Transaction transaction)
        {
            var result = await UpdateAsync(transaction);
            return result;
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            return GetAll();
        }
    }
}
