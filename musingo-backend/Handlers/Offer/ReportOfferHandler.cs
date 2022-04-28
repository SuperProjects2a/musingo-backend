using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class ReportOfferHandler: IRequestHandler<ReportOfferCommand,HandlerResult<Report>>
{
    private readonly IReportRepository _reportRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOfferRepository _offerRepository;

    public ReportOfferHandler(IReportRepository reportRepository, IUserRepository userRepository, IOfferRepository offerRepository)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
        _offerRepository = offerRepository;
    }

    public async Task<HandlerResult<Report>> Handle(ReportOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetOfferById(request.OfferId);

        if (offer is null) return new HandlerResult<Report> { Status = 404 };

        if (await _reportRepository.IsReportedByUser(request.UserId, request.OfferId))
            return new HandlerResult<Report>() { Status = 1 };

        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null) return new HandlerResult<Report> { Status = 404 };

        var report = new Report()
        {
            Reason = Enum.Parse<Reason>(request.Reason),
            Text = request.Text,
            Offer = offer,
            Reporter = user
        };
        await _reportRepository.AddReport(report);

        return new HandlerResult<Report>() { Body = report, Status = 200 };
    }
}