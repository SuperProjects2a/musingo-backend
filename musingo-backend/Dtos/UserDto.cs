namespace musingo_backend.Dtos;

public class UserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public double AvgRating { get; set; }
}