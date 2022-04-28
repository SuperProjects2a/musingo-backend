using Microsoft.EntityFrameworkCore;

namespace musingo_backend.Models;

public enum Gender
{
    CombatHelicopter,
    Male,
    Female,
    Undefined
}
[Flags]
public enum Role
{
    User = 1,
    Moderator = 2,
    Admin = 4
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
    public ICollection<Offer> WatchedOffers { get; set; }
    public ICollection<UserOfferWatch> UserOfferWatches { get; set; }
    public Role Role { get; set; }
    public double WalletBalance { get; set; }
}