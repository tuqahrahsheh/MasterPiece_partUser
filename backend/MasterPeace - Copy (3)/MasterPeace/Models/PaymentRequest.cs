using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class PaymentRequest
{
    public int PaymentRequestId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string CardholderName { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public string ExpiryMonth { get; set; } = null!;

    public string ExpiryYear { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? ServiceType { get; set; }

    public string? SubscriptionType { get; set; }
}
