using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOffersWatchedByUserHandler : IRequestHandler<GetOffersWatchedByUserQuery, ICollection<Offer>?>
{
    private IUserRepository _userRepository;

    public GetOffersWatchedByUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICollection<Offer>?> Handle(GetOffersWatchedByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return null;
        var watchedOffers = user.WatchedOffers;
        return watchedOffers;
    }
}