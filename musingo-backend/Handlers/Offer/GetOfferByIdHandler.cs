using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOfferByIdHandler : IRequestHandler<GetOfferByIdQuery, HandlerResult<OfferDetailsDto>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetOfferByIdHandler(IOfferRepository offerRepository, IImageUrlRepository imageUrlRepository, IUserRepository userRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _imageUrlRepository = imageUrlRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<OfferDetailsDto>> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var offer = await _offerRepository.GetOfferById(request.Id);

        if (offer is null) return new HandlerResult<OfferDetailsDto>() { Status = 404 };

        if (offer.IsBanned) return new HandlerResult<OfferDetailsDto>() { Status = 1 };

        var offerDetailsDto = _mapper.Map<OfferDetailsDto>(offer);
        offerDetailsDto.ImageUrls = _imageUrlRepository.GetImageUrlsByOfferId(offerDetailsDto.Id);
        if (offer.Owner != null) offerDetailsDto.Owner.AvgRating = await _userRepository.GetAvg(offer.Owner.Id);


        return new HandlerResult<OfferDetailsDto>() { Body = offerDetailsDto, Status = 200 };
    }
}