using System.ComponentModel.DataAnnotations;
using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class ChangeRoleDto
{
    [Required]
    public int UserId { get; set; }
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }
}