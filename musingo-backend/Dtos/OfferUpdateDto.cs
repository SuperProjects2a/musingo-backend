namespace musingo_backend.Dtos;

public class OfferUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public double Cost { get; set; }
    public string OfferStatus { get; set; }
    public string ItemCategory { get; set; }
    public string Description { get; set; }
}