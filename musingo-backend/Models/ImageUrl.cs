namespace musingo_backend.Models;

public class ImageUrl
{
    public int Id { get; set; }
    public string Url { get; set; }
    public Offer Offer { get; set; }
}