using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
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
        [HttpPost("add", Name = "AddComment")]
        public async Task<ActionResult<UserCommentDto>> AddComment(UserCommentDto userCommentData)
        {
            var transaction = await _transactionRepository.GetTransaction(userCommentData.TransactionId);
            if (transaction is null) return NotFound();

            if (transaction.Buyer.Email != userCommentData.TransactionBuyer.Email ||
                transaction.Seller.Email != userCommentData.TransactionSeller.Email)
            {
                return Problem("Transaction not found");
            }

            if (transaction.Status != TransactionStatus.Finished)
            {
                return Problem("Transaction not finished. You can't comment yet");
            }

            var isCommented = await _commentRepository.IsCommented(transaction.Id);
            if (isCommented is not null)
            {
                return Problem("You can only comment once");
            }
            var comment = _mapper.Map<UserComment>(userCommentData);
            comment.Transaction = transaction;
            var result = await _commentRepository.AddComment(comment);
            return _mapper.Map<UserCommentDto>(result);
        }
        [HttpPost("update", Name = "UpdateComment")]
        public async Task<ActionResult<UserCommentUpdateDto>> UpdateComment(UserCommentUpdateDto userCommentData)
        {
            var userComment = await _commentRepository.GetCommentById(userCommentData.Id);
            if (userComment is null) { return NotFound(); }
            if (userCommentData.Rating is not null) userComment.Rating = (double)userCommentData.Rating;
            if (!String.IsNullOrEmpty(userCommentData.CommentText)) userComment.CommentText = userCommentData.CommentText;
            // userComment = _mapper.Map<UserComment>(userCommentData);
            var result = await _commentRepository.UpdateComment(userComment);
            return _mapper.Map<UserCommentUpdateDto>(result);
        }
        [HttpDelete("delete", Name = "DeleteComment")]
        public async Task<ActionResult<UserCommentDto>> RemoveCommentById(int id)
        {
            var result = await _commentRepository.RemoveCommentById(id);
            if (result is null) return NotFound();
            return _mapper.Map<UserCommentDto>(result);
        }
        [HttpGet("/avg{id}", Name = "GetAvgRating")]
        public async Task<ActionResult<UserDto>> GetAvgRating(int id)
        {
            var user = await _userRepository.GetUserById(id);
            var rating =await  _commentRepository.GetAvgRating(id);
            var result = _mapper.Map<UserDto>(user);
            result.AvgRating =rating;
            return result;
        }

    }
}
