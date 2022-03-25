using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Dtos;
using musingo_backend.Models;
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

        public CommentController(ICommentRepository commentRepository, ITransactionRepository transactionRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{id}", Name = "GetCommentById")]
        public async Task<ActionResult<UserCommentDto>> GetCommentById(int id)
        {
            var result = await _commentRepository.GetCommentById(id);
            if (result is not null)
            {
                return Ok(_mapper.Map<UserCommentDto>(result));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserCommentCreateDto>> AddComment(UserCommentCreateDto userCommentData)
        {
            var transaction = await _transactionRepository.GetTransaction(userCommentData.TransactionId);
            if (transaction is null) return NotFound();
            if (transaction.Status != TransactionStatus.Finished)
            {
                return Problem("Transaction not finished. You can't comment yet");
            }
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            if (userId != transaction.Buyer.Id && userId != transaction.Seller.Id)
            {
                return Forbid();
            }

            var isCommented = await _commentRepository.IsCommented(transaction.Id, userId);
            if (isCommented is not null)
            {
                return Problem("You can only comment once");
            }
            var comment = _mapper.Map<UserComment>(userCommentData);
            comment.User = await _userRepository.GetUserById(userId);
            comment.Transaction = transaction;
            var result = await _commentRepository.AddComment(comment);
            return _mapper.Map<UserCommentCreateDto>(result);
        }
        [HttpPut]
        public async Task<ActionResult<UserCommentUpdateDto>> UpdateComment(UserCommentUpdateDto userCommentData)
        {
            var userComment = await _commentRepository.GetCommentById(userCommentData.Id);

            if (userComment is null) { return NotFound(); }

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (userComment.User.Id != userId) { return Forbid(); }

            if (userCommentData.Rating is not null) userComment.Rating = (double)userCommentData.Rating;

            if (!String.IsNullOrEmpty(userCommentData.CommentText)) userComment.CommentText = userCommentData.CommentText;

            var result = await _commentRepository.UpdateComment(userComment);
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
