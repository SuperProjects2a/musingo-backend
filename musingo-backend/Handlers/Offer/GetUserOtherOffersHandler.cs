using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserOtherOffersHandler : IRequestHandler<GetUserOtherOffersQuery, HandlerResult<ICollection<OfferDetailsDto>>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IMapper _mapper;

    public GetUserOtherOffersHandler(IOfferRepository offerRepository, IUserRepository userRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _userRepository = userRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }
    public async Task<HandlerResult<ICollection<OfferDetailsDto>>> Handle(GetUserOtherOffersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null)
            return new HandlerResult<ICollection<OfferDetailsDto>>() { Status = 404 };

        var offersDetailDto = _mapper.Map<ICollection<OfferDetailsDto>>(await _offerRepository.GetUserOtherOffers(request.Email,request.OfferId));

        var imageUrlsGroup = _imageUrlRepository.GetImageUrlsByOfferId();
        foreach (var imageUrls in imageUrlsGroup)
        {
            var offer = offersDetailDto.FirstOrDefault(x => x.Id == imageUrls.Key);
            if (offer is not null)
                offer.ImageUrls = imageUrls.Select(x => x.Url);

        }

        return new HandlerResult<ICollection<OfferDetailsDto>>() { Body = offersDetailDto, Status = 200 };

    }
}