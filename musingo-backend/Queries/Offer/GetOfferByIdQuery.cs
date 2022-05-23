using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetOfferByIdQuery : IRequest<HandlerResult<OfferDetailsDto>>
{
    public int Id { get; set; }
}