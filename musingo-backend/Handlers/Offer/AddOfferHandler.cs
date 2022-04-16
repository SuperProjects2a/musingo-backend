using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddOfferHandler: IRequestHandler<AddOfferCommand, HandlerResult<Offer>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IUserRepository _userRepository;

    public AddOfferHandler(IOfferRepository offerRepository, IUserRepository userRepository)
    {
        _offerRepository = offerRepository;
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<Offer>> Handle(AddOfferCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<Offer>();

        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        var offer = new Offer()
        {
            Title = request.Title,
            Cost = request.Cost,
            Description = request.Description,
            OfferStatus = OfferStatus.Active,
            ItemCategory = Enum.Parse<ItemCategory>(request.ItemCategory),
            Owner = await _userRepository.GetUserById(request.UserId)
        };
        if (offer.Owner is null)
        {
            result.Status = 404;
            return result;
        }

        await _offerRepository.AddOffer(offer);

        result.Body = offer;
        return result;
    }
}