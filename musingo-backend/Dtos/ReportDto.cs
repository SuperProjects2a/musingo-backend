namespace musingo_backend.Dtos;

public class ReportDto
{
    public int Id { get; set; }
    public string Reason { get; set; }
    public string? Text { get; set; }
    public UserDto? Reporter { get; set; }
    public OfferDto? Offer { get; set; }
}