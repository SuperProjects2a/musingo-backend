using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Queries;

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
    [HttpPut("watch/{offerId}")]
    public async Task<ActionResult<OfferDetailsDto>> WatchOffer(int offerId)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new AddOfferToWatchedCommand()
        {
            OfferId = offerId,
            UserId = userId
        };
        var result = await _mediator.Send(request);
        switch (result.Status)
        {
            case 403:
                return Forbid();
            case 404:
                return NotFound();
        }
        return _mapper.Map<OfferDetailsDto>(result.Body);

    }

    [Authorize]
    [HttpDelete("watch/{offerId}")]
    public async Task<ActionResult<OfferDetailsDto>> RemoveOfferFromWatched(int offerId)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new RemoveOfferFromWatchedCommand()
        {
            OfferId = offerId,
            UserId = userId
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
    [HttpGet("watch")]
    public async Task<ActionResult<IEnumerable<OfferDto>>> GetUsersWatchedOffers()
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new GetOffersWatchedByUserQuery()
        {
            UserId = userId
        };

        var result = await _mediator.Send(request);
        return Ok(_mapper.Map<IEnumerable<OfferDto>>(result.Body));
    }

}