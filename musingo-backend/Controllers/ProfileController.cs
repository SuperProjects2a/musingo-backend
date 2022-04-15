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
        private IUserRepository _userRepository;

        public ProfileController(IMapper mapper, IMediator mediator, IUserRepository userRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userRepository = userRepository;
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

            if (result is null)
                return NotFound();

            var user = _mapper.Map<UserDetailsDto>(result);
            user.AvgRating = await _userRepository.GetAvg(userId);

            return user;
        }

        [HttpGet("Offers")]
        public async Task<ActionResult<ICollection<OfferDto>>> GetUserOffers()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var request = new GetUserOffersQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);
            if(result is null)
                return NotFound();


            return Ok(_mapper.Map<ICollection<OfferDto>>(result));
            
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

            if(result is null)
                return NotFound();
            return Ok(_mapper.Map<ICollection<UserCommentDto>>(result));
            
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

            if (result is  null)
                return NotFound();

            return Ok(_mapper.Map<ICollection<UserCommentDto>>(result));
        }
        
        [HttpPut]
        public async Task<ActionResult<UserDetailsDto>> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new UpdateUserCommand()
            {
                UserId = userId,
                Email = userUpdateDto.Email,
                NewPassword = userUpdateDto.NewPassword,
                OldPassword = userUpdateDto.OldPassword,
                Name = userUpdateDto.Name,
                Surname = userUpdateDto.Surname,
                Birth = userUpdateDto.Birth,
                City = userUpdateDto.City,
                PostCode = userUpdateDto.PostCode,
                Street = userUpdateDto.Street,
                HouseNumber = userUpdateDto.HouseNumber,
                Gender = userUpdateDto.Gender,
                ImageUrl = userUpdateDto.ImageUrl,
                PhoneNumber = userUpdateDto.PhoneNumber
            };

            var result = await _mediator.Send(request);

            if (result is null)
                NotFound();

            return _mapper.Map<UserDetailsDto>(result);

        }

    }
}
