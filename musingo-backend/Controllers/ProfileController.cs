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
        private IOfferRepository _offerRepository;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        private IJwtAuth _jwtAuth;
        private readonly IMediator _mediator;

        public ProfileController(IMapper mapper, IOfferRepository offerRepository, IUserRepository userRepository, ICommentRepository commentRepository, IJwtAuth jwtAuth, IMediator mediator)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _jwtAuth = jwtAuth;
            _mediator = mediator;
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
        [HttpGet("Profile")]
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
