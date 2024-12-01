using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class MedicationDelivery
{
    public int DeliveryId { get; set; }

    public int? UserId { get; set; }

    public string Address { get; set; } = null!;

    public DateOnly DeliveryDate { get; set; }

    public decimal DeliveryFee { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
