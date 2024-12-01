namespace MasterPeace.DTOs
{
    public class ContactRequestDTO
    {

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Subject { get; set; }

        public string? Message { get; set; }

        public DateTime? DateSent { get; set; }
    }
}
