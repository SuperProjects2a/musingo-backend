namespace musingo_backend.Dtos
{
    public class OfferDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }
        public string OfferStatus { get; set; }
        public string ItemCategory { get; set; }
        public UserDto Owner { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ImageUrls { get; set; }
        public DateTime CreateTime { get; set; }
        public string Email { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public bool isPromoted { get; set; }
    }
}
