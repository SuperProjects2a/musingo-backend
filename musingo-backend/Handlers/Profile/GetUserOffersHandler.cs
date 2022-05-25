using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserOffersHandler: IRequestHandler<GetUserOffersQuery, HandlerResult<ICollection<OfferDto>>>
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

    public async Task<HandlerResult<ICollection<OfferDto>>> Handle(GetUserOffersQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<ICollection<OfferDto>>();
        var offersDto = _mapper.Map<ICollection<OfferDto>>(await _offerRepository.GetUserOffers(request.UserId));

        foreach (var offerDto in offersDto)
        {
            offerDto.ImageUrl = await _imageUrlRepository.GetFirstImageUrlByOfferId(offerDto.Id);
        }

        result.Body = offersDto;
        return result;
    }
}