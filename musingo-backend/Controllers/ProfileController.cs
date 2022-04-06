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
        private IJwtAuth _jwtAuth;

        public ProfileController(IMapper mapper, IOfferRepository offerRepository, IJwtAuth jwt,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _jwtAuth = jwt;
            _userRepository = userRepository;
        }

        [HttpGet]
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

    }
}
