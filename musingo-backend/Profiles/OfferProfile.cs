using AutoMapper;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Profiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferCreateDto, Offer>();
            CreateMap<Offer, OfferDetailsDto>();
        }
    }
}
