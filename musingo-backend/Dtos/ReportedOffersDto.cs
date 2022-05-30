using musingo_backend.Models;

namespace musingo_backend.Dtos;

public class ReportedOffersDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public double Cost { get; set; }
    public string OfferStatus { get; set; }
    public string ItemCategory { get; set; }
    public UserDto Owner { get; set; }
    public bool IsBanned { get; set; }
    public ICollection<ReportShortDto> Reports { get; set; }
}