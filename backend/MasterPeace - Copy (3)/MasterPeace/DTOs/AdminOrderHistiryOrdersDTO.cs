namespace MasterPeace.DTOs
{
    public class AdminOrderHistiryOrdersDTO
    {
        public int UserId { get; set; }

        public int ServiceId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public string? Message { get; set; }

        public bool? Status { get; set; }
    }
}
