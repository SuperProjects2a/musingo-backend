using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Dtos;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IMapper _mapper;
        private IOfferRepository _offerRepository;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        private IJwtAuth _jwtAuth;

        public ProfileController(IMapper mapper, IOfferRepository offerRepository, IJwtAuth jwt,
            IUserRepository userRepository,ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _jwtAuth = jwt;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet("Offers")]
        public async Task<ActionResult<ICollection<OfferDto>>> GetUserOffers()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _offerRepository.GetUserOffers(userId);
            if (result is not null)
            {
                return Ok(_mapper.Map<ICollection<OfferDto>>(result));
            }

            return NotFound();
        }
        [HttpGet("Comments")]
        public async Task<ActionResult<ICollection<UserCommentDto>>> GetUserComments()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _commentRepository.GetUserComments(userId);
            if (result is not null)
            {
                return Ok(_mapper.Map<ICollection<UserCommentDto>>(result));
            }
            return NotFound();
        }
        [HttpGet("Ratings")]
        public async Task<ActionResult<ICollection<UserCommentDto>>> GetUserRatings()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _commentRepository.GetUserRatings(userId);
            if (result is not null)
            {
                return Ok(_mapper.Map<ICollection<UserCommentDto>>(result));
            }
            return NotFound();
        }
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDetailsDto>> GetUserInfo()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _userRepository.GetUserById(userId);
            if (result is not null)
            {
                var user = _mapper.Map<UserDetailsDto>(result);
                user.AvgRating =await  _userRepository.GetAvg(userId);
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<UserUpdateDto>> UpdateUser()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _userRepository.GetUserById(userId);
            if (result is not null)
            {
                var user = _mapper.Map<UserUpdateDto>(result);
                return Ok(user);
            }
            return NotFound();
        }

    }
}
