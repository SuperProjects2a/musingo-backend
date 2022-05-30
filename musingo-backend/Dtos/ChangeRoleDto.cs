using System.ComponentModel.DataAnnotations;
using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class ChangeRoleDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
    public string Email { get; set; }
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }
}