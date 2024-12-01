using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int? UserId { get; set; }

    public string SubscriptionType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
