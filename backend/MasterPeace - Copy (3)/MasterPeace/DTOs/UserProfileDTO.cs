using System.Collections.Generic;

namespace MasterPeace.DTOs
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public List<AppointmentDTO> Appointments { get; set; }

        public List<PaymentDTO> Payments { get; set; }
    }
}
