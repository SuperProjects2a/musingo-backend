﻿using AutoMapper;
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
        private ICommentRepository _commentRepository;
        private IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
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
            var transaction = await _commentRepository.GetTransaction(userCommentData.TransactionId);
            if (transaction is null) return NotFound();

            if (transaction.Buyer.Email != userCommentData.TransactionBuyer.Email ||
            transaction.Seller.Email != userCommentData.TransactionSeller.Email)
            {
                return Problem("Transaction not found");
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
        public async Task<ActionResult<UserCommentDto>> UpdateComment(int id, double? rating, string? text)
        {
            var userComment = await _commentRepository.GetCommentById(id);
            if (userComment is null) { return NotFound(); }

            if (rating is not null) userComment.Rating = (double)rating;
            if (!String.IsNullOrEmpty(text)) userComment.CommentText = text;


            var result = await _commentRepository.UpdateComment(userComment);
            return _mapper.Map<UserCommentDto>(result);
        }
        [HttpDelete("delete", Name = "DeleteComment")]
        public async Task<ActionResult<UserCommentDto>> RemoveCommentById(int id)
        {
            var result = await _commentRepository.RemoveCommentById(id);
            if (result is null) return NotFound();
            return _mapper.Map<UserCommentDto>(result);
        }

    }
}
