using MediatR;

namespace musingo_backend.Queries;

public class GetAllTransactionsQuery : IRequest<HandlerResult<IEnumerable<Models.Transaction>>>
{
    public int UserId { get; set; }
}