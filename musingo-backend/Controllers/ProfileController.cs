using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProfileController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<UserDetailsDto>> GetUserInfo()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new GetUserByIdQuery()
            {
                UserId = userId
            };
            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 404:
                    return NotFound();
            }

            return Ok(result.Body);
        }

        [HttpGet("Offers")]
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetUserOffers()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var request = new GetUserOffersQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);

            return Ok(result.Body);
            
        }

        [HttpGet("Comments")]
        public async Task<ActionResult<ICollection<UserCommentDto>>> GetUserComments()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new GetUserCommentsQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);

            return Ok(_mapper.Map<ICollection<UserCommentDto>>(result.Body));
            
        }
        [HttpGet("Ratings")]
        public async Task<ActionResult<ICollection<UserCommentDto>>> GetUserRatings()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new GetUserRatingsQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);

            return Ok(_mapper.Map<ICollection<UserCommentDto>>(result.Body));
        }
        
        [HttpPut]
        public async Task<ActionResult<UserDetailsDto>> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<UpdateUserCommand>(userUpdateDto);
            request.UserId = userId;

            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 1:
                    return Problem("Wrong current password");
                case 2:
                    return Problem("The new password cannot be the same as the old one");
                case 3:
                    return Problem("Someone else is using this email already");
                case 404:
                    return NotFound();
            }


            return Ok(_mapper.Map<UserDetailsDto>(result.Body));

        }

    }
}
