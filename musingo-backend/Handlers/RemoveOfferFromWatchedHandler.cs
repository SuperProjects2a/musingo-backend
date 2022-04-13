using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class RemoveOfferFromWatchedHandler : IRequestHandler<RemoveOfferFromWatchedCommand, Offer?>
{
    private IUserRepository _userRepository;

    public RemoveOfferFromWatchedHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Offer?> Handle(RemoveOfferFromWatchedCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return null;

        var offerToDelete = user.WatchedOffers.FirstOrDefault(x => x.Id == request.OfferId);
        if (offerToDelete is null) return null;
        
        user.WatchedOffers.Remove(offerToDelete);
        await _userRepository.UpdateUser(user);
        
        return offerToDelete;
    }
}