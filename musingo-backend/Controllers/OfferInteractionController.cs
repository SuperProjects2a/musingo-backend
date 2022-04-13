using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Dtos;

namespace musingo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfferInteractionController : ControllerBase
{
    private IMediator _mediator;
    private IMapper _mapper;

    public OfferInteractionController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<OfferDetailsDto>> WatchOffer(int offerId)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new AddOfferToWatchedCommand()
        {
            OfferId = offerId,
            UserId = userId
        };
        var result = await _mediator.Send(request);
        if (result is null) return NotFound();
        return _mapper.Map<OfferDetailsDto>(result);

    }
}