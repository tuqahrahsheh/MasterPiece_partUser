using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class Hospital
{
    public int HospitalId { get; set; }

    public string HospitalName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string? Services { get; set; }

    public double? Rating { get; set; }

    public string? PhoneNumber { get; set; }

    public string? WorkingHours { get; set; }
}
