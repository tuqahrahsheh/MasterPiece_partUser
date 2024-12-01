using System;

namespace MasterPeace.DTOs
{
   
        public class AppointmentDTO
        {
            public int AppointmentID { get; set; }
            public int UserID { get; set; }
            public int ServiceID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public DateOnly Date { get; set; }
            public TimeOnly Time { get; set; }
            public string Message { get; set; }
            public bool? Status { get; set; }
            public string UserName { get; set; }
            public string ServiceName { get; set; }
        }
    }
