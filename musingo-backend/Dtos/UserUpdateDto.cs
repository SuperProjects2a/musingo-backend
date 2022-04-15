using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class UserUpdateDto
{
    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,60}")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,60}$")]
    public string? Surname { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    public string OldPassword { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string NewPassword { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    public string? ImageUrl { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[0-9]{9,9}$")]
    public string? PhoneNumber { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^([a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ\-]+\s)*[-\a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,60}$")]
    public string? City { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,60}$")]
    public string? Street { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[1-9][0-9]{0,5}$")]
    public string? HouseNumber { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [RegularExpression(@"^[1-9][0-9]\-[1-9][0-9]{2,2}$")]
    public string? PostCode { get; set; }

    [Required(AllowEmptyStrings = true)]
    [DefaultValue("")]
    [EnumDataType(typeof(Gender))]
    public string? Gender { get; set; }

    [DefaultValue(null)]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1/1/1900", "1/1/2022", ErrorMessage = "Date is out of Range")]
    public DateTime? Birth { get; set; }
}
