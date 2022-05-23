using AutoMapper;
using MediatR;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class AddOfferHandler: IRequestHandler<AddOfferCommand, HandlerResult<Offer>>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IImageUrlRepository _imageUrlRepository;
    private readonly IMapper _mapper;

    public AddOfferHandler(IOfferRepository offerRepository, IUserRepository userRepository, IImageUrlRepository imageUrlRepository, IMapper mapper)
    {
        _offerRepository = offerRepository;
        _userRepository = userRepository;
        _imageUrlRepository = imageUrlRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<Offer>> Handle(AddOfferCommand request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<Offer>();

        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        var offer = new Offer()
        {
            Title = request.Title,
            Cost = request.Cost,
            Description = request.Description,
            OfferStatus = OfferStatus.Active,
            ItemCategory = Enum.Parse<ItemCategory>(request.ItemCategory),
            Owner = await _userRepository.GetUserById(request.UserId),
            Email = request.Email,
            City = request.City,
            PhoneNumber = request.PhoneNumber
        };
        if (offer.Owner is null)
        {
            result.Status = 404;
            return result;
        }

        await _offerRepository.AddOffer(offer);

        var imageUrls = new List<ImageUrl>();
        foreach (var requestImageUrl in request.ImageUrls)
        {
            imageUrls.Add(new ImageUrl() { Url = requestImageUrl, Offer = offer });
        }
        await _imageUrlRepository.AddRangeImageUrl(imageUrls);

        result.Body = offer;
        return result;
    }
}