namespace musingo_backend.Dtos;

public class TransactionDetailsDto
{
    public int Id { get; set; }
    public OfferDto? Offer { get; set; }
    public UserDto? Seller { get; set; }
    public UserDto? Buyer { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public string Status { get; set; }
    public double Cost { get; set; }
}