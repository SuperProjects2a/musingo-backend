namespace musingo_backend.Models;

public enum TransactionStatus
{
    Opened,
    UnderNegotiation,
    Finished,
    Declined
}
public class Transaction
{
    public int Id { get; set; }
    public Offer? Offer { get; set; }
    public User? Seller { get; set; }
    public User? Buyer { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public TransactionStatus Status { get; set; }
    public double Cost { get; set; }
    public ICollection<Message> Messages { get; set; }
}