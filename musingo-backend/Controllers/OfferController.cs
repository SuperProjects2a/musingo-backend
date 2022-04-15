using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Commands;
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
        private IMediator _mediator;

        public OfferController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
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
            var request = new GetOfferByIdQuery()
            {
                Id = id
            };
            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<OfferDetailsDto>(result));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OfferDetailsDto>> Add([FromBody] OfferCreateDto offerCreateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new AddOfferCommand()
            {
                UserId = userId,
                Title = offerCreateDto.Title,
                Cost = offerCreateDto.Cost,
                Description = offerCreateDto.Description,
                ItemCategory = offerCreateDto.ItemCategory
            };

            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<OfferDetailsDto>(result));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<OfferDetailsDto>> Update([FromBody] OfferUpdateDto offerUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new UpdateOfferCommand()
            {
                UserId = userId,
                OfferId = offerUpdateDto.Id,
                Title = offerUpdateDto.Title,
                Description = offerUpdateDto.Description,
                Cost = offerUpdateDto.Cost,
                ImageUrl = offerUpdateDto.ImageUrl,
                ItemCategory = offerUpdateDto.ItemCategory,
                OfferStatus = offerUpdateDto.OfferStatus
            };

            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound();

            return Ok(result);
        }


    }
}
