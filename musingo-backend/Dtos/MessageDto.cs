﻿using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class MessageDto
{
    public int Id { get; set; }
    public UserDto Sender { get; set; }
    public DateTime? SendTime { get; set; }
    public string? Text { get; set; }
    public bool? IsRead { get; set; }
}