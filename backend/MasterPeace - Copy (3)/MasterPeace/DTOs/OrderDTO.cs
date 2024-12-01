namespace MasterPeace.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string MedicationName { get; set; }
        public DateTime DeliveryTime { get; set; }

        // Include necessary properties from MedicalSuppliesRequest
        public MedicalSuppliesRequestDTO MedicalSuppliesRequest { get; set; }
    }
}
