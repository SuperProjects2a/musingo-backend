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
    [ResponseCache(CacheProfileName = "Default30")]
    public async Task<ActionResult<IEnumerable<OfferDto>>> GetUsersWatchedOffers()
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new GetOffersWatchedByUserQuery()
        {
            UserId = userId
        };

        var result = await _mediator.Send(request);
        var dtoRes = _mapper.Map<IEnumerable<OfferDto>>(result.Body);
        if (result is not null && result.Body is not null)
        {
            var arr = dtoRes.ToArray();
            for (int i = 0; i < result.Body.Count; i++)
            {
                arr[i].isWatched = true;
            }

            dtoRes = arr;
        }
        
        return Ok(dtoRes);
    }
    
    [Authorize]
    [HttpPost("{offerId}/openTransaction")]
    public async Task<ActionResult<TransactionDetailsDto>> OpenTransaction([FromRoute]int offerId, [FromQuery] string? message)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new OpenTransactionCommand()
        {
            UserId = userId,
            OfferId = offerId,
            Message = message ?? ""
        };

        var result = await _mediator.Send(request);
        return result.Status switch
        {
            404 => NotFound(),
            2 => Problem("Cannot open transaction for this item"),
            200 => Ok(_mapper.Map<TransactionDetailsDto>(result.Body)),
            _ => Forbid()
        };
    }
    
    [Authorize]
    [HttpPost("{offerId}/buy")]
    public async Task<ActionResult<TransactionDetailsDto>> BuyWithoutNegotiation(int offerId)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new PurchaseCommand()
        {
            UserId = userId,
            OfferId = offerId
        };

        var result = await _mediator.Send(request);
        return result.Status switch
        {
            404 => NotFound(),
            1 => Problem("Not enough wallet balance"),
            2 => Problem("Cannot buy this item"),
            200 => Ok(_mapper.Map<TransactionDetailsDto>(result.Body)),
            _ => Forbid()
        };

    }

}