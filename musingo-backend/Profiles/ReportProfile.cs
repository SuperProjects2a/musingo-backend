using AutoMapper;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Profiles;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report, ReportDto>().ReverseMap();
        CreateMap<Report, ReportShortDto>().ReverseMap();
    }
}