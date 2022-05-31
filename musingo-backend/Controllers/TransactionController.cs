using System.Diagnostics;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private IMapper _mapper;
    private IMediator _mediator;
    private IImageUrlRepository _imageUrlRepository;
    private IUserCommentRepository _userCommentRepository;


    public TransactionController(IMapper mapper, IMediator mediator, IImageUrlRepository imageUrlRepository, IUserCommentRepository userCommentRepository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _imageUrlRepository = imageUrlRepository;
        _userCommentRepository = userCommentRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDetailsDto>>> GetTransactions()
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new GetAllTransactionsQuery() { UserId = userId };
        var result = await _mediator.Send(request);

        var arr = result.Body.ToArray();
        var ratings = (await _userCommentRepository.GetUserComments(userId)).ToArray();
        var rateValues = new int[arr.Length];
        for (int i = 0; i < result.Body.Count(); i++)
        {
            if (ratings.Any(x => x.Transaction.Id == arr[i].Id))
            {
                rateValues[i] = Convert.ToInt32(ratings.First(x => x.Transaction.Id == arr[i].Id).Rating);
            }
        }

        var dto = _mapper.Map<IEnumerable<TransactionDetailsDto>>(result.Body);
        var dtoArr = dto.ToArray();
        for (int i = 0; i < rateValues.Length; i++)
        {
            dtoArr[i].Rating = rateValues[i];
        }
        
        return Ok(dtoArr);
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

    [Authorize]
    [HttpGet("{transactionId}")]
    public async Task<ActionResult<TransactionDetailsDto>> GetTransactionById(int transactionId)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var request = new GetTransactionQuery() { Id = transactionId };
        var result = await _mediator.Send(request);
        if (result.Body is not null)
        {
            if (result.Body.Buyer.Id != userId && result.Body.Seller.Id != userId) 
                return Forbid();
        }

        var dto = _mapper.Map<TransactionDetailsDto>(result.Body);
        dto.Offer.ImageUrls = _imageUrlRepository.GetImageUrlsByOfferId(dto.Offer.Id);

        return result.Status switch
        {
            404 => NotFound(),
            200 => Ok(dto),
            _ => Forbid()
        };
    }
}