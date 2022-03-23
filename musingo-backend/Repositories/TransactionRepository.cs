

using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories
{
    public interface ITransactionRepository
    {
        public Task<Transaction> GetTransaction(int id);
    }
    public class TransactionRepository:Repository<Transaction>,ITransactionRepository
    {
        public TransactionRepository(RepositoryContext context) : base(context) { }
        public async  Task<Transaction> GetTransaction(int id)
        {
            var result = await repositoryContext.Transactions
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;


        }
    }
}
