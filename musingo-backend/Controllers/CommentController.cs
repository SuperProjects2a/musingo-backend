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
        private readonly ICommentRepository _commentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CommentController(ICommentRepository commentRepository, ITransactionRepository transactionRepository, IUserRepository userRepository, IMapper mapper, IMediator mediator)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
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

            if (result is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserCommentDto>(result));
            
        }

        [HttpPost]
        public async Task<ActionResult<UserCommentCreateDto>> AddComment(UserCommentCreateDto userCommentData)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new AddCommentCommand()
            {
                CommentText = userCommentData.CommentText,
                Rating = userCommentData.Rating,
                TransactionId = userCommentData.TransactionId,
                UserId = userId
            };

            var result = await _mediator.Send(request);
            if (result is null)
                return NotFound();

            return _mapper.Map<UserCommentCreateDto>(result);
        }
        [HttpPut]
        public async Task<ActionResult<UserCommentUpdateDto>> UpdateComment(UserCommentUpdateDto userCommentData)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new UpdateCommentCommand()
            {
                CommentId = userCommentData.Id,
                CommentText = userCommentData.CommentText,
                Rating = userCommentData.Rating,
                UserId = userId
            };
            var result = await _mediator.Send(request);

            if (result is null)
                return NotFound();

            return _mapper.Map<UserCommentUpdateDto>(result);
        }
        [HttpDelete]
        public async Task<ActionResult<UserCommentDto>> RemoveCommentById(int id)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var commentToRemove = await _commentRepository.GetCommentById(id);

            if (commentToRemove is null) { return NotFound(); }

            if (commentToRemove.User.Id != userId) { return Forbid(); }

            var result = await _commentRepository.RemoveCommentById(commentToRemove);
            if (result is null) return NotFound();
            return _mapper.Map<UserCommentDto>(result);
        }

    }
}
