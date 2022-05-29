using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOffersWatchedByUserHandler : IRequestHandler<GetOffersWatchedByUserQuery, HandlerResult<ICollection<OfferDetailsDto>>>
{
    private IUserRepository _userRepository;
    private IImageUrlRepository _imageUrlRepository;
    private IMapper _mapper;

    public GetOffersWatchedByUserHandler(IUserRepository userRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<ICollection<OfferDetailsDto>>> Handle(GetOffersWatchedByUserQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<ICollection<OfferDetailsDto>>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return null;
        var watchedOffers = user.WatchedOffers.Where(x=>x.OfferStatus == OfferStatus.Active);

        var offersDetailsDto = _mapper.Map<ICollection<OfferDetailsDto>>(watchedOffers);
        var imageUrlsGroup = _imageUrlRepository.GetImageUrlsByOfferId();
        foreach (var imageUrls in imageUrlsGroup)
        {
            var offer = offersDetailsDto.FirstOrDefault(x => x.Id == imageUrls.Key);
            if (offer is not null)
                offer.ImageUrls = imageUrls.Select(x => x.Url);

        }
        result.Body = offersDetailsDto;
        return result;
    }
}