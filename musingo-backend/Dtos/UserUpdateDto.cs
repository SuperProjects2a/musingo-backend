using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos;

public class UserUpdateDto
{
    [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
    public string Email { get; set; }

    [RegularExpression(@"^[a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,24}")]
    [MaxLength(24)]
    [MinLength(3)]
    public string Name { get; set; }
    public string Password { get; set; }

    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string NewPassword { get; set; }
    public string? ImageUrl { get; set; }

    [RegularExpression(@"^[a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,24}")]
    [MaxLength(24)]
    [MinLength(3)]
    public string? Surname { get; set; }

    [RegularExpression(@"^[0-9]*$")]
    [MaxLength(9)]
    public string? PhoneNumber { get; set; }

    [RegularExpression(@"/^([a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ\-]+\s)*[-\a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]+$/i")]
    public string? City { get; set; }

    [RegularExpression(@"^[a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,60}")]
    public string? Street { get; set; }

    [RegularExpression(@"^[1-9][0-9]*$")]
    [MaxLength(5)]
    [MinLength(1)]
    public string? HouseNumber { get; set; }

    [RegularExpression(@"[1-9][0-9]\-[1-9][0-9]{2}")]
    public string? PostCode { get; set; }

    public string? Gender { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Birth { get; set; }
}