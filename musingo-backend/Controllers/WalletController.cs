using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Dtos;

namespace musingo_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private IMapper _mapper;
    private IMediator _mediator;

    public WalletController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("AddBalance")]
    public async Task<ActionResult<UserDetailsDto>> AddWalletBalance(int amount)
    {
        var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
        var allowedValues = new[] {20, 50, 100, 200, 500};
        if (allowedValues.All(x => x != amount)) return BadRequest();

        var request = new AddBalanceCommand() {UserId = userId, AmountToAdd = amount};
        var result = await _mediator.Send(request);
        if (result.Status == 404) return NotFound();
        return Ok(_mapper.Map<UserDetailsDto>(result.Body));
    }
}