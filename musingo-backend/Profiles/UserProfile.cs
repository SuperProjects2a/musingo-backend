using AutoMapper;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Migrations;
using musingo_backend.Models;

namespace musingo_backend.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, UserRegisterDto>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserDetailsDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserRegisterDto, RegisterUserCommand>().ReverseMap();
        CreateMap<UserUpdateDto, UpdateUserCommand>().ReverseMap();


    }
}