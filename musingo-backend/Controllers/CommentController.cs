using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CommentController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<ActionResult<UserCommentDto>> GetCommentById(int id)
        {
            var request = new GetCommentByIdQuery
            {
                CommentId = id
            };

            var result = await _mediator.Send(request);

            if (result.Status == 404)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserCommentDto>(result.Body));

        }

        [HttpPost]
        public async Task<ActionResult<UserCommentCreateDto>> AddComment(UserCommentCreateDto userCommentData)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<AddCommentCommand>(userCommentData);
            request.UserId = userId;

            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 404:
                    return NotFound();
                case 1:
                    return Problem("Transaction is not finished");
                case 2:
                    return Problem("You are not buyer or seller");
                case 3:
                    return Problem("You can comment only once");

                    
            }

            return _mapper.Map<UserCommentCreateDto>(result.Body);
        }
        [HttpPut]
        public async Task<ActionResult<UserCommentUpdateDto>> UpdateComment(UserCommentUpdateDto userCommentData)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<UpdateCommentCommand>(userCommentData);
            request.UserId = userId;
            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 403:
                    return Forbid();
                case 404:
                    return NotFound();
            }

            return Ok(_mapper.Map<UserCommentUpdateDto>(result.Body));
        }
        [HttpDelete]
        public async Task<ActionResult<UserCommentDto>> RemoveCommentById(int id)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new RemoveCommentCommand()
            {
                CommentId = id,
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

            return Ok(_mapper.Map<UserCommentDto>(result.Body));
        }

    }
}
