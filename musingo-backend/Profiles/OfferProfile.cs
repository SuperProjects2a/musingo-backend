using AutoMapper;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;

namespace musingo_backend.Profiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferCreateDto, Offer>();
            CreateMap<Offer, OfferDetailsDto>();
            CreateMap<OfferUpdateDto, UpdateOfferCommand>().ReverseMap();
            CreateMap<OfferCreateDto, AddOfferCommand>().ReverseMap();
            CreateMap<OfferFilterDto, GetOffersByFilterQuery>().ReverseMap();
        }
    }
}
