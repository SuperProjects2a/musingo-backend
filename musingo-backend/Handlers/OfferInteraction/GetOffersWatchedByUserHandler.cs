using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOffersWatchedByUserHandler : IRequestHandler<GetOffersWatchedByUserQuery, HandlerResult<ICollection<OfferDto>>>
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

    public async Task<HandlerResult<ICollection<OfferDto>>> Handle(GetOffersWatchedByUserQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<ICollection<OfferDto>>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null) return null;
        var watchedOffers = user.WatchedOffers;

        var offersDto = _mapper.Map<ICollection<OfferDto>>(watchedOffers);
        foreach (var offerDto in offersDto)
        {
            offerDto.ImageUrl = await _imageUrlRepository.GetFirstImageUrlByOfferId(offerDto.Id);
        }
        result.Body = offersDto;
        return result;
    }
}