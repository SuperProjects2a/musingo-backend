using AutoMapper;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<UserComment, UserCommentDto>().ReverseMap();
            CreateMap<UserComment, UserCommentUpdateDto>();
            CreateMap<UserCommentUpdateDto, UserComment>();
        }
    }
}
