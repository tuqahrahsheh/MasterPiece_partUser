using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int RequestId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual MedicalSuppliesRequest Request { get; set; } = null!;
}
