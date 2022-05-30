using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetPromotedOffersHandler : IRequestHandler<GetPromotedOffersQuery, HandlerResult<ICollection<OfferDetailsDto>>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IMapper _mapper;

    public GetPromotedOffersHandler(IOfferRepository offerRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }
    public async Task<HandlerResult<ICollection<OfferDetailsDto>>> Handle(GetPromotedOffersQuery request, CancellationToken cancellationToken)
    {
        var offersDetailsDto = _mapper.Map<ICollection<OfferDetailsDto>>(await _offerRepository.GetPromotedOffers());
        var imageUrlsGroup = _imageUrlRepository.GetImageUrlsByOfferId();
        foreach (var imageUrls in imageUrlsGroup)
        {
            var offer = offersDetailsDto.FirstOrDefault(x => x.Id == imageUrls.Key);
            if (offer is not null)
                offer.ImageUrls = imageUrls.Select(x => x.Url);

        }

        return new HandlerResult<ICollection<OfferDetailsDto>>() { Body = offersDetailsDto, Status = 200 };
    }
}