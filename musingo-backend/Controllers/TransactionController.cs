using System.Diagnostics;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
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
    [HttpGet]
    [ResponseCache(CacheProfileName = "Default30")]
    public async Task<ActionResult<IEnumerable<TransactionDetailsDto>>> GetTransactions()
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new GetAllTransactionsQuery() {UserId = userId};
        var result = await _mediator.Send(request);

        return Ok(_mapper.Map<IEnumerable<TransactionDetailsDto>>(result.Body));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<TransactionDetailsDto>> UpdateTransaction(
        [FromBody] TransactionUpdateDto transactionUpdateDto)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var parseSuccess = Enum.TryParse<TransactionStatus>(transactionUpdateDto.TransactionStatus, out var status);
        if (!parseSuccess) return BadRequest();

        var request = new UpdateTransactionCommand()
        {
            TransactionId = transactionUpdateDto.TransactionId,
            Cost = transactionUpdateDto.Cost,
            TransactionStatus = status,
            UserId = userId
        };
        var result = await _mediator.Send(request);

        return result.Status switch
        {
            3 => Problem("Cannot manage finished transaction"),
            200 => Ok(result.Body),
            404 => NotFound(),
            403 => Forbid(),
            _ => Forbid()
        };

    }
    
    [Authorize]
    [HttpPost("{transactionId}/buy")]
    public async Task<ActionResult<TransactionDetailsDto>> BuyNegotiated(int transactionId)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new PurchaseFromTransactionCommand()
        {
            UserId = userId,
            TransactionId = transactionId
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