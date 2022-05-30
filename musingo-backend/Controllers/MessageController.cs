using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands.MessageC;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries.MessageQ;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MessageController(IMapper mapper, IMediator mediator, IMessageRepository messageRepository)
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
                200 => Ok(result.Body),
                403 => Forbid(),
                404 => NotFound(),
                _ => Forbid()
            };
        }
        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage(MessageSendDto message)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var request = new SendMessageCommand()
            {
                TransactionId = message.TransactionId,
                UserId = userId,
                Text = message.Text
            };
            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(_mapper.Map<MessageDto>(result.Body)),
                403 => Forbid(),
                404 => NotFound(),
                _ => Forbid()
            };
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MessageChatDto>>> ChatMessages()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var request = new GetAllChatsQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(result.Body),
                404 => NotFound(),
                _ => Forbid()
            };
        }

    }
}