using MediatR;
using musingo_backend.Models;

namespace musingo_backend.Commands;

public class UpdateUserCommand: IRequest<User?>
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ImageUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? PostCode { get; set; }
    public string? Gender { get; set; }
    public DateTime? Birth { get; set; }
}