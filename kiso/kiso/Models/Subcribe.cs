using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Subcribe
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreateDate { get; set; }
}
