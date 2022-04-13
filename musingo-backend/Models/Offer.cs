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
    public string? ImageUrl { get; set; }
    public double Cost { get; set; }
    public User? Owner { get; set; }
    public string? Description { get; set; }
    public OfferStatus OfferStatus { get; set; }
    public ItemCategory ItemCategory { get; set; }
    public DateTime? CreateTime { get; set; }
    public ICollection<User> Watchers { get; set; }
    public ICollection<UserOfferWatch> UserOfferWatches { get; set; }
}