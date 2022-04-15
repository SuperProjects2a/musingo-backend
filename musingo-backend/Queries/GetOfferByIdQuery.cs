using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Queries;

public class GetOfferByIdQuery : IRequest<Offer?>
{
    public int Id { get; set; }
}