namespace musingo_backend.Models;

public class UserOfferWatch
{
    public User User { get; set; }
    public int UserId { get; set; }
    public Offer Offer { get; set; }
    public int OfferId { get; set; }
}