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
            var request = _mapper.Map<GetOffersByFilterQuery>(filterDto);
            var result = await _mediator.Send(request);
            return Ok(_mapper.Map<IEnumerable<OfferDetailsDto>>(result.Body));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDetailsDto>> GetById(int id)
        {
            var request = new GetOfferByIdQuery()
            {
                Id = id
            };

            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 404:
                    return NotFound();
            }

            return Ok(_mapper.Map<OfferDetailsDto>(result.Body));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OfferDetailsDto>> Add([FromBody] OfferCreateDto offerCreateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<AddOfferCommand>(offerCreateDto);
            request.UserId = userId;

            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 404:
                    return NotFound();
            }

            return Ok(_mapper.Map<OfferDetailsDto>(result.Body));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<OfferDetailsDto>> Update([FromBody] OfferUpdateDto offerUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<UpdateOfferCommand>(offerUpdateDto);
            request.UserId= userId;

            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 403:
                    return Forbid();
                case 404:
                    return NotFound();
            }

            return Ok(result.Body);
        }


    }
}
