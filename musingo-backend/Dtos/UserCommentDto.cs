﻿using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class UserCommentDto
{
    public int TransactionId { get; set; }
    public double Rating { get; set; }
    public string? CommentText { get; set; }
    public UserDto? TransactionSeller { get; set; }
    public UserDto? TransactionBuyer { get; set; }
}