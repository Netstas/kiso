using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Ques { get; set; } = null!;

    public string Reply { get; set; } = null!;

    public bool Active { get; set; }

    public int Sort { get; set; }
}
