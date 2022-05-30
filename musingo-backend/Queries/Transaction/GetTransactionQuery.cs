using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetTransactionQuery : IRequest<HandlerResult<Transaction>>
{
    public int Id { get; set; }
}