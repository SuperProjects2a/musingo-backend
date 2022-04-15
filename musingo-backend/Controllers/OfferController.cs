using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
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
        private IMediator _mediator;
        private IJwtAuth _jwtAuth;

        public OfferController(IMapper mapper, IOfferRepository offerRepository, IJwtAuth jwt, IUserRepository userRepository, IMediator mediator)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _jwtAuth = jwt;
            _userRepository = userRepository;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetAll([FromQuery] OfferFilterDto filterDto)
        {
            var request = new GetOffersByFilterQuery
            {
                Search = filterDto.Search,
                Category = filterDto.Category,
                PriceFrom = filterDto.PriceFrom,
                PriceTo = filterDto.PriceTo,
                Sorting = filterDto.Sorting
            };

            var result = await _mediator.Send(request);
            return Ok(_mapper.Map<IEnumerable<OfferDetailsDto>>(result));
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

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<OfferDetailsDto>> Update([FromBody] OfferUpdateDto offerUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var offer = await _offerRepository.GetOfferById(offerUpdateDto.Id);
            if (offer is null) return NotFound();
            if (offer.Owner?.Id != userId) return Forbid();
            if (offer.OfferStatus == OfferStatus.Sold || offer.OfferStatus == OfferStatus.Cancelled)
            {
                return Forbid();
            }

            offer.Title = offerUpdateDto.Title;
            offer.Cost = offerUpdateDto.Cost;
            offer.ImageUrl = offerUpdateDto.ImageUrl;
            if (Enum.TryParse<OfferStatus>(offerUpdateDto.OfferStatus, out var status)) offer.OfferStatus = status;
            else return BadRequest();
            if (Enum.TryParse<ItemCategory>(offerUpdateDto.ItemCategory, out var category)) offer.ItemCategory = category;
            else return BadRequest();

            offer.Description = offerUpdateDto.Description;

            var result = await _offerRepository.UpdateOffer(offer);
            return Ok(result);
        }


    }
}
