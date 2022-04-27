using AutoMapper;
using musingo_backend.Commands.MessageC;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Profiles;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>().ReverseMap();
    }
}