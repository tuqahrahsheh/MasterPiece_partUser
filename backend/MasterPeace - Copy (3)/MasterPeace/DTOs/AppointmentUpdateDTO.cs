using System;

namespace MasterPeace.DTOs
{
    public class AppointmentUpdateDTO
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
