namespace musingo_backend.Models;

public enum OfferStatus
{
    Active,
    Reserved,
    Sold,
    Inactive,
    Cancelled
}

public enum ItemCategory
{
    Other,
    Guitars,
    WindInstruments,
    Keyboards,
    Percussion,
    String,
    Microphones,
    Headphones,
    NotesBooks,
    Accessories,
}

public class Offer
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public double Cost { get; set; }
    public User? Owner { get; set; }
    public string? Description { get; set; }
    public OfferStatus OfferStatus { get; set; }
    public ItemCategory ItemCategory { get; set; }
    public DateTime? CreateTime { get; set; }
    public ICollection<User> Watchers { get; set; }
    public ICollection<UserOfferWatch> UserOfferWatches { get; set; }
    public bool IsBanned { get; set; }
    public string Email { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
    public bool isPromoted { get; set; }
}