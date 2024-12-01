using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class HomeVisit
{
    public int VisitId { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public DateOnly VisitDate { get; set; }

    public TimeOnly VisitTime { get; set; }

    public decimal ServiceFee { get; set; }

    public decimal? DiscountAmount { get; set; }

    public string? Status { get; set; }

    public virtual HomeDoctor? Doctor { get; set; }

    public virtual User? User { get; set; }
}
