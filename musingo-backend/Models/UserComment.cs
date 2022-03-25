namespace musingo_backend.Models;

public class UserComment
{
    public int Id { get; set; }
    public double Rating { get; set; }
    public Transaction? Transaction { get; set; }
    public string? CommentText { get; set; }
    public User? User { get; set; }
    
}