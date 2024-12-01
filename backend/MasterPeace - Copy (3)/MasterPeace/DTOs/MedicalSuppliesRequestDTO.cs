namespace MasterPeace.DTOs
{
    public class MedicalSuppliesRequestDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string MedicationName { get; set; } = null!;

        public DateTime DeliveryTime { get; set; }

        public IFormFile PrescriptionFile { get; set; } 

    }
}
    