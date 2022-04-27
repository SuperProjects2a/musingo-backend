using AutoMapper;
using MediatR;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, HandlerResult<UserDetailsDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<HandlerResult<UserDetailsDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new HandlerResult<UserDetailsDto>();
        var user = await _userRepository.GetUserById(request.UserId);
        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        var userDto = _mapper.Map<UserDetailsDto>(user);
        userDto.AvgRating = await _userRepository.GetAvg(user.Id);
        result.Body = userDto;
        return result;
    }
}