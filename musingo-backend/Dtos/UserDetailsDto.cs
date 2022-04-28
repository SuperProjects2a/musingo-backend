namespace musingo_backend.Dtos
{
    public class UserDetailsDto
    {
        // dane ktore widzi tylko uzytkownik
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }
        public double AvgRating { get; set; }
        public double WalletBalance { get; set; }

        // dane do wysylki
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostCode { get; set; }

        // dane opcjonalne
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birth { get; set; }
        public bool IsBanned { get; set; }
    }
}
