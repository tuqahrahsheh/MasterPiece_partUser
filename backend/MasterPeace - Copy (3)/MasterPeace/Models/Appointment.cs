using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int UserId { get; set; }

    public int ServiceId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Message { get; set; }

    public bool? Status { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
