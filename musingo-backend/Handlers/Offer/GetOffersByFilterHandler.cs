﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetOffersByFilterHandler : IRequestHandler<GetOffersByFilterQuery, HandlerResult<ICollection<Offer>>>
{
    private readonly IOfferRepository _offerRepository;

    public GetOffersByFilterHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }
    public async Task<HandlerResult<ICollection<Offer>>> Handle(GetOffersByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<ICollection<Offer>>();

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
        result.Body = await offers.ToListAsync();
        return result;
    }
}