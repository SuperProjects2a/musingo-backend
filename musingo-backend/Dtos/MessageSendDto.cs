using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos;

public class MessageSendDto
{
    [Required]
    public int TransactionId { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Text { get; set; }
}