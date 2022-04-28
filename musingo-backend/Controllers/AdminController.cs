using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using musingo_backend.Commands.Admin;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdminController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddRole")]

        public async Task<ActionResult<UserDto>> AddRole(ChangeRoleDto addRole)
        {
            var request = new AddRoleCommand()
            {
                UserId = addRole.UserId,
                Role = addRole.Role
            };
            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(_mapper.Map<UserDto>(result.Body)),
                404 => NotFound(),
                _ => Forbid()
            };
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RemoveRole")]
        public async Task<ActionResult<UserDto>> RemoveRole(ChangeRoleDto removeRole)
        {
            var request = new RemoveRoleCommand()
            {
                UserId = removeRole.UserId,
                Role = removeRole.Role
            };
            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(_mapper.Map<UserDto>(result.Body)),
                404 => NotFound(),
                _ => Forbid()
            };
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("UserBanUnban/{userId}")]
        public async Task<ActionResult<UserDetailsDto>> UserBanUnban(int userId)
        {
            var request = new BanUnbanUserCommand()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(_mapper.Map<UserDetailsDto>(result.Body)),
                404 => NotFound(),
                _ => Forbid()
            };

        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("OfferBanUnban/{offerId}")]
        public async Task<ActionResult<OfferDto>> OfferBanUnban(int offerId)
        {
            var request = new BanUnbanOfferCommand()
            {
                OfferId = offerId
            };

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(_mapper.Map<OfferDto>(result.Body)),
                404 => NotFound(),
                _ => Forbid()
            };

        }

    }
}
