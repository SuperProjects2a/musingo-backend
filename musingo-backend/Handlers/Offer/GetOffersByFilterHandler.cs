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

        var offers = _offerRepository.GetAllActiveOffers();

        if (!string.IsNullOrWhiteSpace(request.Search))
            offers = offers.Where(x => x.Title.Contains(request.Search));

        if (!string.IsNullOrWhiteSpace(request.Category))
            offers = offers.Where(x => x.ItemCategory == Enum.Parse<ItemCategory>(request.Category));

        if (request.PriceFrom is not null)
            offers = offers.Where(x => x.Cost >= request.PriceFrom);

        if (request.PriceTo is not null)
            offers = offers.Where(x => x.Cost <= request.PriceTo);

        offers = request.Sorting switch
        {
            nameof(Sorting.Latest) => offers.OrderByDescending(x => x.CreateTime),
            nameof(Sorting.Oldest) => offers.OrderBy(x => x.CreateTime),
            nameof(Sorting.Ascending) => offers.OrderBy(x => x.Cost),
            nameof(Sorting.Descending) => offers.OrderBy(x => x.Cost),
            _ => throw new ArgumentException()
        };
        var o = await offers.ToListAsync();
        var offersDetailDto = _mapper.Map<ICollection<OfferDetailsDto>>(o);
        var t =  _imageUrlRepository.GetImageUrlsByOfferId();
        foreach (var test in t)
        {
            var offer = offersDetailDto.FirstOrDefault(x => x.Id == test.Key);
            offer.ImageUrls = test.Select(x => x.Url);

        }
        return new HandlerResult<ICollection<OfferDetailsDto>>(){Body = offersDetailDto,Status = 200};
    }
}