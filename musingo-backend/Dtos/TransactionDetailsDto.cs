namespace musingo_backend.Dtos;

public class TransactionDetailsDto
{
    public OfferDto? Offer { get; set; }
    public UserDto? Seller { get; set; }
    public UserDto? Buyer { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public string Status { get; set; }
    public double Cost { get; set; }
}