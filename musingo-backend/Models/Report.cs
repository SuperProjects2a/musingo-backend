namespace musingo_backend.Models;

public enum Reason
{
    Insults,
    ViolationsOfMusingoRules,
    Others
}


public class Report
{
    public int Id { get; set; }
    public Reason Reason { get; set; }
    public string? Text { get; set; }
    public User? Reporter { get; set; }
    public Offer? Offer { get; set; }
}