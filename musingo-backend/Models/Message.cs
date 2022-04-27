namespace musingo_backend.Models;

public class Message
{
    public int Id { get; set; }
    public Transaction? Transaction { get; set; }
    public User? Sender { get; set; }
    public DateTime? SendTime { get; set; }
    public string? Text { get; set; }
    public bool? IsRead { get; set; }
    
}