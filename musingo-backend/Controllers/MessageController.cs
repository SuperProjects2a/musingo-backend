using System.Diagnostics;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries.Message;

namespace musingo_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MessageController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(template:"{id}")]
        public async Task<ActionResult<ICollection<MessageDto>>> GetMessagesByTransactionId(int id)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var request = new GetMessagesByTransactionIdQuery()
            {
                TransactionId = id,
                UserId = userId
            };
            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(_mapper.Map<ICollection<MessageDto>>(result.Body)),
                403 => Forbid(),
                404 => NotFound(),
                _ => Forbid()
            };
        }

    }
}