using AutoMapper;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Profiles;

public class ImageUrlProfile : Profile
{
    public ImageUrlProfile() 
    {
        CreateMap<ImageUrl, ImageUrlDto>().ReverseMap();
    }
}