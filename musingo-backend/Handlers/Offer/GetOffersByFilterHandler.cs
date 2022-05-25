using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOffersByFilterHandler : IRequestHandler<GetOffersByFilterQuery, HandlerResult<ICollection<OfferDetailsDto>>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private IMapper _mapper;

    public GetOffersByFilterHandler(IOfferRepository offerRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }
    public async Task<HandlerResult<ICollection<OfferDetailsDto>>> Handle(GetOffersByFilterQuery request, CancellationToken cancellationToken)
    {

        var offersQ = _offerRepository.GetAllActiveOffers();

        if (!string.IsNullOrWhiteSpace(request.Search))
            offersQ = offersQ.Where(x => x.Title.Contains(request.Search));

        if (!string.IsNullOrWhiteSpace(request.Category))
            offersQ = offersQ.Where(x => x.ItemCategory == Enum.Parse<ItemCategory>(request.Category));

        if (request.PriceFrom is not null)
            offersQ = offersQ.Where(x => x.Cost >= request.PriceFrom);

        if (request.PriceTo is not null)
            offersQ = offersQ.Where(x => x.Cost <= request.PriceTo);

        offersQ = request.Sorting switch
        {
            nameof(Sorting.Latest) => offersQ.OrderByDescending(x => x.CreateTime),
            nameof(Sorting.Oldest) => offersQ.OrderBy(x => x.CreateTime),
            nameof(Sorting.Ascending) => offersQ.OrderBy(x => x.Cost),
            nameof(Sorting.Descending) => offersQ.OrderByDescending(x => x.Cost),
            _ => throw new ArgumentException()
        };
        var offerts = await offersQ.ToListAsync();
        var offersDetailDto = _mapper.Map<ICollection<OfferDetailsDto>>(offerts);
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