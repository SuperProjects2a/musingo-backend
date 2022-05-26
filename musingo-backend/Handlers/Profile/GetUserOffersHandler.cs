using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserOffersHandler: IRequestHandler<GetUserOffersQuery, HandlerResult<ICollection<OfferDetailsDto>>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IMapper _mapper;

    public GetUserOffersHandler(IOfferRepository offerRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<ICollection<OfferDetailsDto>>> Handle(GetUserOffersQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<ICollection<OfferDetailsDto>>();
        var offersDetailDto = _mapper.Map<ICollection<OfferDetailsDto>>(await _offerRepository.GetUserOffers(request.UserId));

        var imageUrlsGroup = _imageUrlRepository.GetImageUrlsByOfferId();
        foreach (var imageUrls in imageUrlsGroup)
        {
            var offer = offersDetailDto.FirstOrDefault(x => x.Id == imageUrls.Key);
            if (offer is not null)
                offer.ImageUrls = imageUrls.Select(x => x.Url);

        }

        result.Body = offersDetailDto;
        return result;
    }
}