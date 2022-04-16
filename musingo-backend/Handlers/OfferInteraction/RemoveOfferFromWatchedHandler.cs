using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class RemoveOfferFromWatchedHandler : IRequestHandler<RemoveOfferFromWatchedCommand, HandlerResult<Offer>>
{
    private IUserRepository _userRepository;

    public RemoveOfferFromWatchedHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<Offer>> Handle(RemoveOfferFromWatchedCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<Offer>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        var offerToDelete = user.WatchedOffers.FirstOrDefault(x => x.Id == request.OfferId);
        if (offerToDelete is null)
        {
            result.Status = 404;
            return result;
        }
        
        user.WatchedOffers.Remove(offerToDelete);
        await _userRepository.UpdateUser(user);
        result.Body = offerToDelete;
        return result;
    }
}