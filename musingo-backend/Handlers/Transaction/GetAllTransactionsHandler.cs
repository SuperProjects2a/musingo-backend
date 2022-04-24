using MediatR;
using Microsoft.EntityFrameworkCore;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, HandlerResult<IEnumerable<Transaction>>>
{
    private IUserRepository _userRepository;
    private ITransactionRepository _transactionRepository;

    public GetAllTransactionsHandler(IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<HandlerResult<IEnumerable<Transaction>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        var transactionsQuery = _transactionRepository.GetAllTransactions();
        var transactions = await transactionsQuery
            .Include(x => x.Buyer)
            .Include(x => x.Seller)
            .Where(x => x.Buyer.Id == user.Id || x.Seller.Id == user.Id)
            .Include(x => x.Offer)
            .ToListAsync(cancellationToken);

        return new HandlerResult<IEnumerable<Transaction>>() {Body = transactions, Status = 200};
    }
}