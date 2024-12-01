using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class MedicalSuppliesRequest
{
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string MedicationName { get; set; } = null!;

    public DateTime DeliveryTime { get; set; }

    public string? PrescriptionFilePath { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
