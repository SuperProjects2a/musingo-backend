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

    }
}
