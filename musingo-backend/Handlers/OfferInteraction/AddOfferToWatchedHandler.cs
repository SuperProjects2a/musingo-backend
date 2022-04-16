using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddOfferToWatchedHandler : IRequestHandler<AddOfferToWatchedCommand, HandlerResult<Offer>>
{
    private IUserRepository _userRepository;
    private IOfferRepository _offerRepository;
    
    public AddOfferToWatchedHandler(IUserRepository userRepository, IOfferRepository offerRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _offerRepository = offerRepository;
    }

    public async Task<HandlerResult<Offer>> Handle(AddOfferToWatchedCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<Offer>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }
        var offers = user.WatchedOffers;
        if (offers.Any(x => x.Id == request.OfferId))
        {
            result.Status = 404;
            return result;
        }
        var offerToAdd = await _offerRepository.GetOfferById(request.OfferId);
        if (offerToAdd is null)
        {
            result.Status = 404;
            return result;
        }

        if (offerToAdd.Owner?.Id == request.UserId)
        {
            result.Status = 403;
            return result;
        }
        
        user.WatchedOffers.Add(offerToAdd);
        await _userRepository.UpdateUser(user);
        result.Body = offerToAdd;

        return result;
    }
}