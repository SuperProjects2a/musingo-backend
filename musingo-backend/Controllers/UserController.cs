using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Dtos;
using musingo_backend.Models;
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

    public UserController(IMapper mapper, IUserRepository userRepository,IJwtAuth jwtAuth)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _jwtAuth = jwtAuth;
    }
    
    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserDto>> GetPlatformById(int id)
    {
        var platformItem = await _userRepository.GetUserById(id);
        if (platformItem is not null)
        {
            return Ok(_mapper.Map<UserDto>(platformItem));
        }
        return NotFound();
    }
    [AllowAnonymous]
    [HttpPost("login", Name = "LoginUser")]
    public async Task<ActionResult<UserDto>> LoginUser(UserLoginDto loginData)
    {
        if (loginData.Email is null || loginData.Password is null) return BadRequest();
        var user = await _userRepository.LoginUser(loginData.Email, loginData.Password);
        if (user is null) return NotFound();
        var t = _mapper.Map<UserDto>(user);
        t.Token = _jwtAuth.Authentication(t.Email);
        return t;
        //return _mapper.Map<UserDto>(user);

    }

    [HttpPost("register", Name = "RegisterUser")]
    public async Task<ActionResult<UserDto>> RegisterUser(UserRegisterDto userRegisterData)
    {
        var user = _mapper.Map<User>(userRegisterData);
        var result = await _userRepository.AddUser(user);
        if (user is null) return Forbid();
        return _mapper.Map<UserDto>(user);
    }


}