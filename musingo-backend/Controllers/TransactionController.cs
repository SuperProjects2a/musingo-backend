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
public class TransactionController : ControllerBase
{
    private IMapper _mapper;
    private IMediator _mediator;

    public TransactionController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("buy")]
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

    [Authorize]
    [HttpPost("open")]
    public async Task<ActionResult<TransactionDetailsDto>> OpenTransaction(int offerId, string? message)
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDetailsDto>>> GetTransactions()
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new GetAllTransactionsQuery() {UserId = userId};
        var result = await _mediator.Send(request);

        return Ok(_mapper.Map<IEnumerable<TransactionDetailsDto>>(result.Body));
    }
}