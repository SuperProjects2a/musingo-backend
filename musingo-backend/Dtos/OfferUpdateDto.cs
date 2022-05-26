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

    [Required(ErrorMessage = "This value is required.")]
    [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
    public string Email { get; set; }
    [RegularExpression(@"^([a-zA-Z¹êó³¿Ÿæñœ¥ÊÓ¯ÆÑ£Œ\-]+\s)*[-\a-zA-Z¹êó³¿Ÿæñœ¥ÊÓ¯ÆÑ£Œ]{3,60}$")]
    public string? City { get; set; }
    [RegularExpression(@"^[0-9]{9,9}$")]
    public string? PhoneNumber { get; set; }
}