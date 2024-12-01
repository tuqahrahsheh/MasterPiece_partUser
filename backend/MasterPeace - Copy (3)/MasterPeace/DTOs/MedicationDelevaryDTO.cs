namespace MasterPeace.DTOs
{
    public class MedicationDelevaryDTO
    {

        public int? UserId { get; set; }

        public string Address { get; set; } = null!;

        public DateOnly DeliveryDate { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

    }
}
