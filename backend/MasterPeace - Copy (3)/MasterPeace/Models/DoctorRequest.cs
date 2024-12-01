using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class DoctorRequest
{
    public int RequestId { get; set; }

    public string Name { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public string? Description { get; set; }

    public string ContactEmail { get; set; } = null!;

    public DateTime? RequestDate { get; set; }

    public string? DoctorImage { get; set; }

    public string Status { get; set; } = null!;
}
