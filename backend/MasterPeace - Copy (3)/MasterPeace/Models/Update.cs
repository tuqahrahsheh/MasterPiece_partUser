using System;
using System.Collections.Generic;

namespace MasterPeace.Models;

public partial class Update
{
    public int UpdateId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Date { get; set; }
}
