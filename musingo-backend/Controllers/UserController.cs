using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IJwtAuth _jwtAuth;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper, IUserRepository userRepository, IJwtAuth jwtAuth, IMediator mediator)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _jwtAuth = jwtAuth;
        _mediator = mediator;
    }
    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var request = new GetUserByIdQuery()
        {
            UserId = id
        };

        var result = await _mediator.Send(request);

        if (result is null)
            return NotFound();

        var user = _mapper.Map<UserDto>(result);
        user.AvgRating = await _userRepository.GetAvg(id);

        return user;

    }
    [AllowAnonymous]
    [HttpPost("login", Name = "LoginUser")]
    public async Task<ActionResult<UserDto>> LoginUser(UserLoginDto loginData)
    {
        var request = new LoginUserCommand()
        {
            Email = loginData.Email,
            Password = loginData.Password
        };

        var result = await _mediator.Send(request);

        if (result is null)
            return NotFound();

        var token = _jwtAuth.Authentication(result);
        HttpContext.Response.Headers.Add("AuthToken", token);

        return Ok(_mapper.Map<UserDto>(result));

    }
    [AllowAnonymous]
    [HttpPost("register", Name = "RegisterUser")]
    public async Task<ActionResult<UserDto>> RegisterUser(UserRegisterDto userRegisterData)
    {
        var request = _mapper.Map<RegisterUserCommand>(userRegisterData);

        var result = await _mediator.Send(request);
        if (result is null)
            return NotFound();

        return _mapper.Map<UserDto>(result);
    }
}