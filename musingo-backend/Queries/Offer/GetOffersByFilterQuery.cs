using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetOffersByFilterQuery : IRequest<HandlerResult<ICollection<OfferDetailsDto>>>
{
    public string? Search { get; set; }
    public string? Category { get; set; }
    public double? PriceFrom { get; set; }
    public double? PriceTo { get; set; }
    public string? Sorting { get; set; }
}