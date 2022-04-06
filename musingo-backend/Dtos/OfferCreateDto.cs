using System.ComponentModel.DataAnnotations;

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
        public string ItemCategory { get; set; }
    }
}
