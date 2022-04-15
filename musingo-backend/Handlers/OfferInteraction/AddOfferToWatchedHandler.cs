using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddOfferToWatchedHandler : IRequestHandler<AddOfferToWatchedCommand, Offer?>
{
    private IUserRepository _userRepository;
    private IOfferRepository _offerRepository;
    
    public AddOfferToWatchedHandler(IUserRepository userRepository, IOfferRepository offerRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _offerRepository = offerRepository;
    }

    public async Task<Offer?> Handle(AddOfferToWatchedCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return null;
        var offers = user.WatchedOffers;
        if (offers.Any(x => x.Id == request.OfferId)) return null;
        var offerToAdd = await _offerRepository.GetOfferById(request.OfferId);
        if (offerToAdd is null) return null;
        if (offerToAdd.Owner?.Id == request.UserId) return null;
        
        user.WatchedOffers.Add(offerToAdd);
        await _userRepository.UpdateUser(user);

        return offerToAdd;
    }
}