using System;

namespace MasterPeace.DTOs
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}
