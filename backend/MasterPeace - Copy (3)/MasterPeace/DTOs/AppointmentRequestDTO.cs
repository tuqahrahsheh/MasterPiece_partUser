using System;
using System.ComponentModel.DataAnnotations;

namespace MasterPeace.DTOs
{
    public class AppointmentRequestDTO
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int ServiceID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public string Message { get; set; }
    }
}
