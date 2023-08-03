using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Ward
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Sort { get; set; }

    public bool Active { get; set; }

    public string? Prefix { get; set; }

    public int DistrictId { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
