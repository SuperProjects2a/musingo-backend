using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos;

public class UserRegisterDto
{
    // dane osobowe - wymagane
    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[a-zA-ZπÍÛ≥øüÊÒú• ”Øè∆—£å]{3,24}")]
    [MaxLength(24)]
    [MinLength(3)]
    public string Name { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[a-zA-ZπÍÛ≥øüÊÒú• ”Øè∆—£å]{3,24}")]
    [MaxLength(24)]
    [MinLength(3)]
    public string Surname { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
    public string Email { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[0-9]*$")]
    [MaxLength(9)]
    [MinLength(9)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [Range(typeof(bool), "true", "true")]
    // akceptowanie TOS
    public bool AcceptedTOS { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string Password { get; set; }

}