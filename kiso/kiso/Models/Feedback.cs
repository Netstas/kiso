using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Classify { get; set; }

    public int Star { get; set; }

    public string? Content { get; set; }

    public string? Image { get; set; }

    public int Sort { get; set; }

    public bool Active { get; set; }
}
