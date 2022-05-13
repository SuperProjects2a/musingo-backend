using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Dtos;
using musingo_backend.Queries;

namespace musingo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IMapper _mapper;
        private IMediator _mediator;

        public TestController(IMapper mapper, IMediator mediator)
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
    }
}
