using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos;

public class UserLoginDto
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; } 
}