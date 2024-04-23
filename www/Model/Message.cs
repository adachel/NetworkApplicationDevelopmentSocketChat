using System;
using System.Collections.Generic;

namespace www.Model;

public partial class Message
{
    public int Id { get; set; }

    public string Message1 { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
