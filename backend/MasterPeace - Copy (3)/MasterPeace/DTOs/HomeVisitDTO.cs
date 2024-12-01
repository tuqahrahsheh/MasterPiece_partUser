namespace MasterPeace.DTOs
{
    public class HomeVisitDTO
    {

        public int? UserId { get; set; }

        public int? DoctorId { get; set; }

        public DateOnly VisitDate { get; set; }

        public TimeOnly VisitTime { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal? DiscountAmount { get; set; }

        public string? Status { get; set; }
    }
}
