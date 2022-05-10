using MediatR;
using musingo_backend.Dtos;

namespace musingo_backend.Queries;

public class GetReportedOffersQuery: IRequest<HandlerResult<ICollection<ReportedOffersDto>>>
{
    
}