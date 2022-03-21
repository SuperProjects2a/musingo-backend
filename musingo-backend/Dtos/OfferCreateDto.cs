namespace musingo_backend.Dtos
{
    public class OfferCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string ItemCategory { get; set; }
    }
}
