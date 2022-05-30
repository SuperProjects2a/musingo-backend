using System.ComponentModel.DataAnnotations;
using musingo_backend.Models;

namespace musingo_backend.Dtos
{
    public class OfferCreateDto
    {
        [MaxLength(50)]
        [MinLength(5)]
        public string Title { get; set; }
        [MaxLength(9000)]
        public string Description { get; set; }
        [Range(0d, 1000000d)]
        public double Cost { get; set; }
        [EnumDataType(typeof(ItemCategory))]
        public string ItemCategory { get; set; }
        public ICollection<string> ImageUrls { get; set; }

        [Required(ErrorMessage = "This value is required.")]
        [RegularExpression(@"^[a-zA-Z0-9\.\-_]{1,}@[a-zA-Z0-9\-_]{1,}\.[a-zA-Z\.]{1,}$")]
        public string Email { get; set; }
        [RegularExpression(@"^([a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ\-]+\s)*[-\a-zA-ZąęółżźćńśĄĘÓŻŹĆŃŁŚ]{3,60}$")]
        public string? City { get; set; }
        [RegularExpression(@"^[0-9]{9,9}$")]
        public string? PhoneNumber { get; set; }
    }
}
