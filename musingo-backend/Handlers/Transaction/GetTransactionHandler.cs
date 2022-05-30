using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, HandlerResult<Transaction>>
{
    private ITransactionRepository _transactionRepository;

    public GetTransactionHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<HandlerResult<Transaction>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetTransaction(request.Id);
        if (transaction is null)
        {
            return new HandlerResult<Transaction>()
            {
                Body = null,
                Status = 404
            };
        }

        return new HandlerResult<Transaction>()
        {
            Body = transaction,
            Status = 200
        };
    }
}