using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Sort { get; set; }

    public bool Active { get; set; }

    public string? Prefix { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
