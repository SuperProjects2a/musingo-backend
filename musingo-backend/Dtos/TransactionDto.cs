namespace musingo_backend.Dtos;

public class TransactionDto
{
    public int Id { get; set; }
    public OfferDto? Offer { get; set; }
    public UserDto? Seller { get; set; }
    public UserDto? Buyer { get; set; }
}