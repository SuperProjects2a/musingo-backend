using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetReportedOffersHandler: IRequestHandler<GetReportedOffersQuery,HandlerResult<ICollection<ReportedOffersDto>>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IMapper _mapper;

    public GetReportedOffersHandler(IOfferRepository offerRepository, IReportRepository reportRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _reportRepository = reportRepository;
        _mapper = mapper;
    }


    public async Task<HandlerResult<ICollection<ReportedOffersDto>>> Handle(GetReportedOffersQuery request, CancellationToken cancellationToken)
    {
        var reportedOffers = await _offerRepository.GetReportedOffers();

        var reportedOffersDto = _mapper.Map<ICollection<ReportedOffersDto>>(reportedOffers);

        foreach (var reportedOfferDto in reportedOffersDto)
        {
            reportedOfferDto.Reports = _mapper.Map<ICollection<ReportShortDto>>(await _reportRepository.GetReportsByOffer(reportedOfferDto.Id));
        }

        return new HandlerResult<ICollection<ReportedOffersDto>>() { Body = reportedOffersDto, Status = 200 };
    }
}