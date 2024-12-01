using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class HomeDoctor
{
    public int DoctorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public int ExperienceYears { get; set; }

    public string? PhoneNumber { get; set; }

    public double? Rating { get; set; }

    public string? Availability { get; set; }

    public virtual ICollection<HomeVisit> HomeVisits { get; set; } = new List<HomeVisit>();
}
