using MediatR;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOffersWatchedByUserHandler : IRequestHandler<GetOffersWatchedByUserQuery, HandlerResultCollection<Offer>>
{
    private IUserRepository _userRepository;

    public GetOffersWatchedByUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResultCollection<Offer>> Handle(GetOffersWatchedByUserQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResultCollection<Offer>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return null;
        var watchedOffers = user.WatchedOffers;
        result.Body = watchedOffers;
        return result;
    }
}