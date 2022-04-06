using Microsoft.EntityFrameworkCore;

namespace musingo_backend.Models;

public enum Gender
{
    CombatHelicopter,
    Male,
    Female,
    Undefined
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string? ImageUrl { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? PostCode { get; set; }
    public Gender Gender { get; set; }
    public DateTime? Birth { get; set; }
}