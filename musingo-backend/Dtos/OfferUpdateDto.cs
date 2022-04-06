using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos;

public class OfferUpdateDto
{
    public int Id { get; set; }
    [MaxLength(50)]
    [MinLength(5)]
    public string Title { get; set; }
    [MaxLength(200)]
    public string? ImageUrl { get; set; }
    [Range(0d, 1000000d)]
    public double Cost { get; set; }
    public string OfferStatus { get; set; }
    public string ItemCategory { get; set; }
    [MaxLength(9000)]
    public string Description { get; set; }
}