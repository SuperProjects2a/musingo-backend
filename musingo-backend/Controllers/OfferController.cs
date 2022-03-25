using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private IMapper _mapper;
        private IOfferRepository _offerRepository;
        private IUserRepository _userRepository;
        private IJwtAuth _jwtAuth;

        public OfferController(IMapper mapper, IOfferRepository offerRepository, IJwtAuth jwt, IUserRepository userRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _jwtAuth = jwt;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<OfferDto>>> GetAll()
        {
            var result = await _offerRepository.GetAllOffers();
            return Ok(_mapper.Map<ICollection<OfferDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDetailsDto>> GetById(int id)
        {
            var result = await _offerRepository.GetOfferById(id);
            if (result is null) return NotFound();
            return Ok(_mapper.Map<OfferDetailsDto>(result));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OfferDetailsDto>> Add([FromBody] OfferCreateDto offerCreateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var user = await _userRepository.GetUserById(userId);
            var offer = _mapper.Map<Offer>(offerCreateDto);
            offer.Owner = user;
            offer.OfferStatus = OfferStatus.Active;

            var result = await _offerRepository.AddOffer(offer);
            return Ok(_mapper.Map<OfferDetailsDto>(result));
        }
    }
}
