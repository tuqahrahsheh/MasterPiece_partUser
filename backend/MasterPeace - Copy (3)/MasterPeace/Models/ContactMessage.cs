using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class ContactMessage
{
    public int MessageId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime DateSent { get; set; }
}
