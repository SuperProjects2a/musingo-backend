namespace musingo_backend.Dtos;

public class UserDto
{
    // dane ktore widza wszyscy
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public double AvgRating { get; set; }
    public string? PhoneNumber { get; set; }

}