using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
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
    private IMapper _mapper;
    private IUserRepository _userRepository;
    private IJwtAuth _jwtAuth;
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
        if (loginData.Email is null || loginData.Password is null) return BadRequest();
        var user = await _userRepository.LoginUser(loginData.Email, loginData.Password);
        if (user is null) return NotFound();
        var token = _jwtAuth.Authentication(user);
        HttpContext.Response.Headers.Add("AuthToken", token);

        var userDto = _mapper.Map<UserDto>(user);
        userDto.AvgRating = await _userRepository.GetAvg(user.Id);
        return Ok(userDto);

    }
    [AllowAnonymous]
    [HttpPost("register", Name = "RegisterUser")]
    public async Task<ActionResult<UserDto>> RegisterUser(UserRegisterDto userRegisterData)
    {
            var user = _mapper.Map<User>(userRegisterData);
            var result = await _userRepository.AddUser(user);
            if (result is null) return ValidationProblem();
            return _mapper.Map<UserDto>(user);
    }
}