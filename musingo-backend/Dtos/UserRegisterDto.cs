using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos;

public class UserRegisterDto
{

    // dane osobowe - wymagane
    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[a-zA-Z¹êó³¿Ÿæñœ¥ÊÓ¯ÆÑ£Œ]{3,24}")]
    [MaxLength(24)]
    [MinLength(3)]
    public string Name { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[a-zA-Z¹êó³¿Ÿæñœ¥ÊÓ¯ÆÑ£Œ]{3,24}")]
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

    //public string City { get; set; } // wybieramy z listy

    //[RegularExpression(@"^[a-zA-Z¹êó³¿Ÿæñœ¥ÊÓ¯ÆÑ£Œ]{3,24}")]
    //public string Street { get; set; }

    //[RegularExpression(@"^[0-9]*$")]
    //public string HouseNumber { get; set; }

    //[RegularExpression(@"^[0-9]*$")]
    //public string PostCode { get; set; }

    // dane osobowe - niewymagane
    // public DateTime? Birth { get; set; } // wybieramy w kalendarzu na stronie czy cos
    // public string? Gender { get; set; } // wybieramy z listy

}